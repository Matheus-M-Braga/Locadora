#pragma warning disable CS8604
using AutoMapper;
using Library.Business.Models.Dtos.Rental;
using Library.Business.Interfaces.IRepository;
using Library.Business.Interfaces.IServices;
using Library.Business.Pagination;
using Library.Business.Models;
using Library.Business.Models.Dtos.Validations;
using Library.Business.Models.Dtos;

namespace Library.Business.Services
{
    public class RentalsService : IRentalsService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RentalsService(IRentalRepository repo, IBookRepository bookRepo, IUserRepository userRepo, IMapper mapper)
        {
            _rentalRepository = repo;
            _bookRepository = bookRepo;
            _userRepository = userRepo;
            _mapper = mapper;
        }

        public async Task<ResultService<List<RentalDto>>> GetAll(FilterDb filterDb)
        {
            var rentals = await _rentalRepository.GetAllRentalsPaged(filterDb);
            var result = new PagedBaseResponseDto<RentalDto>(rentals.TotalRegisters, rentals.TotalPages, rentals.Page, _mapper.Map<List<RentalDto>>(rentals.Data));

            if (result.Data.Count == 0)
                return ResultService.Fail<List<RentalDto>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.Page, result.TotalPages);
        }

        public async Task<ResultService<List<RentalCountDto>>> GetAllCount()
        {
            var rentals = await _rentalRepository.GetAllRentals();
            if (rentals.Count < 1) return ResultService.Fail<List<RentalCountDto>>("Não foram encontrados alguéis.");
            var rentalsDashDto = _mapper.Map<List<RentalCountDto>>(rentals);
            return ResultService.Ok(rentalsDashDto);
        }

        public async Task<ResultService<RentalDto>> GetById(int id)
        {
            var rental = await _rentalRepository.GetRentalById(id);
            if (rental == null) return ResultService.Fail<RentalDto>("Aluguel não encontrado!");
            var rentalDto = _mapper.Map<RentalDto>(rental);
            return ResultService.Ok(rentalDto);
        }

        public async Task<ResultService> Create(CreateRentalDto model)
        {
            var validation = new RentalDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            var rental = _mapper.Map<Rentals>(model);

            var book = await _bookRepository.GetBookById(rental.BookId);
            if (book == null)
                return ResultService.Fail<CreateRentalDto>("Livro não encontrado!");

            var user = await _userRepository.GetUserById(rental.UserId);
            if (user == null)
                return ResultService.Fail<CreateRentalDto>("Usuário não encontrado!");

            if (rental.RentalDate.Date != DateTime.Now.Date)
                return ResultService.Fail<CreateRentalDto>("Data de aluguel não pode ser diferente da data de Hoje!");

            bool? forecastValidate = await _rentalRepository.CheckForecastDate(rental.ForecastDate, rental.RentalDate);
            if (forecastValidate == true)
                return ResultService.Fail<UpdateRentalDto>("Prazo do aluguel não pode ser superior a 30 dias!");
            else if (forecastValidate == false)
                return ResultService.Fail<UpdateRentalDto>("Data de Previsão não pode ser anterior à Data do Aluguel!");

            var userRental = await _rentalRepository.GetRentalByUserIdandBookId(book.Id, user.Id);
            if (userRental.Count > 0)
                return ResultService.Fail<UpdateRentalDto>("Usuário já possui aluguel desse livro!");

            var updateBook = await _bookRepository.UpdateQuantity(book.Id, false);
            if (updateBook == false)
                return ResultService.Fail<UpdateRentalDto>("Livro com estoque esgotado.");

            rental.Status = "Pendente";

            await _rentalRepository.Add(rental);

            return ResultService.Ok("Aluguel adicionado com êxito.");
        }

        public async Task<ResultService> Update(UpdateRentalDto model)
        {
            var result = await _rentalRepository.GetRentalById((int)model.Id);
            if (result == null)
                return ResultService.Fail<UpdateRentalDto>("Aluguel não encontrado!");

            var rental = _mapper.Map(model, result);

            var validation = new UpdateRentalDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            if (rental.ReturnDate.Value.Date != DateTime.Now.Date)
                return ResultService.Fail<CreateRentalDto>("Data de devolução não pode ser diferente da data de Hoje!");

            rental.Status = await _rentalRepository.GetStatus(rental.ForecastDate, rental.ReturnDate);

            await _rentalRepository.Update(rental);

            bool updateBook = await _bookRepository.UpdateQuantity(rental.BookId, true);
            if (updateBook == false)
                return ResultService.Fail<UpdateRentalDto>("Livro com estoque esgotado.");

            return ResultService.Ok("Devolução realizada com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var rental = await _rentalRepository.GetRentalById(id);
            if (rental == null)
                return ResultService.Fail<RentalDto>("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.Fail<RentalDto>("Aluguel foi devolvido, não pode ser deletado.");

            await _rentalRepository.Delete(rental);

            return ResultService.Ok("Aluguel deletado com êxito!");
        }
    }
}
