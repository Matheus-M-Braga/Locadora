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
            var result = new PagedBaseResponseDto<RentalDto>(rentals.TotalRegisters, rentals.TotalPages, rentals.PageNumber, _mapper.Map<List<RentalDto>>(rentals.Data));

            if (result.Data.Count == 0) return ResultService.NotFound<List<RentalDto>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.PageNumber, result.TotalPages);
        }

        public async Task<ResultService<RentalDto>> GetById(int id)
        {
            var rental = await _rentalRepository.GetRentalById(id);
            if (rental == null) return ResultService.NotFound<RentalDto>("Aluguel não encontrado!");

            var rentalDto = _mapper.Map<RentalDto>(rental);
            return ResultService.Ok(rentalDto);
        }

        public async Task<ResultService> Create(CreateRentalDto model)
        {
            var validation = new RentalDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            var rental = _mapper.Map<Rentals>(model);

            var book = await _bookRepository.GetBookById(rental.BookId);
            if (book == null) return ResultService.NotFound<CreateRentalDto>("Livro não encontrado!");

            var user = await _userRepository.GetUserById(rental.UserId);
            if (user == null) return ResultService.NotFound<CreateRentalDto>("Usuário não encontrado!");

            if (rental.RentalDate.Date != DateTime.Now.Date) return ResultService.BadRequest("Data de aluguel não pode ser diferente da data de Hoje!");

            var diff = rental.ForecastDate.Subtract(rental.RentalDate);
            if (diff.Days > 30) return ResultService.BadRequest("Prazo do aluguel não pode ser superior a 30 dias!");

            if (rental.ForecastDate < rental.RentalDate) return ResultService.BadRequest("Data de Previsão não pode ser anterior à Data do Aluguel!");

            var userRental = await _rentalRepository.GetRentalByUserIdandBookId(book.Id, user.Id);
            if (userRental.Count > 0) return ResultService.BadRequest("Usuário já possui aluguel desse livro!");

            bool updateBook = await _bookRepository.UpdateQuantity(rental.BookId);
            if (updateBook == false) return ResultService.BadRequest("Livro com estoque esgotado.");

            rental.Status = "Pendente";
            await _rentalRepository.Add(rental);

            return ResultService.Created("Aluguel adicionado com êxito.");
        }

        public async Task<ResultService> Update(UpdateRentalDto model)
        {
            var result = await _rentalRepository.GetRentalById((int)model.Id);
            if (result == null) return ResultService.NotFound<UpdateRentalDto>("Aluguel não encontrado!");

            var rental = _mapper.Map(model, result);

            var validation = new UpdateRentalDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            if (rental.ReturnDate.Value.Date != DateTime.Now.Date) return ResultService.BadRequest("Data de devolução não pode ser diferente da data de Hoje!");

            if (rental.ForecastDate < rental.ReturnDate) 
            { 
                rental.Status = "Atrasado"; 
            }
            else 
            { 
                rental.Status = "No prazo"; 
            }

            await _rentalRepository.Update(rental);
            await _bookRepository.UpdateQuantity(rental.BookId, true);

            return ResultService.Ok("Devolução realizada com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var rental = await _rentalRepository.GetRentalById(id);
            if (rental == null) return ResultService.NotFound<RentalDto>("Aluguel não encontrado!");


            await _rentalRepository.Delete(rental);
            await _bookRepository.UpdateQuantity(rental.BookId, true);

            return ResultService.Ok("Aluguel deletado com êxito!");
        }
    }
}
