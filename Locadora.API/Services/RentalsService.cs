#pragma warning disable CS8604
using AutoMapper;
using Locadora.API.Dtos;
using Locadora.API.Pagination;
using Locadora.API.Models;
using Locadora.API.Validations;
using Locadora.API.Interfaces.IRepository;
using Locadora.API.Interfaces.IServices;
using Locadora.API.Dtos.Rental;

namespace Locadora.API.Services
{
    public class RentalsService : IRentalsService
    {
        private readonly IRentalRepository _repo;
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public RentalsService(IRentalRepository repo, IBookRepository bookRepo, IUserRepository userRepo, IMapper mapper)
        {
            _repo = repo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ResultService<List<RentalDto>>> GetAll(FilterDb filterDb)
        {
            var rentals = await _repo.GetAllRentals(filterDb);
            var result = new PagedBaseResponseDto<RentalDto>(rentals.TotalRegisters, rentals.TotalPages, _mapper.Map<List<RentalDto>>(rentals.Data));

            if (result.Data.Count == 0)
                return ResultService.Fail<List<RentalDto>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.TotalPages);
        }

        public async Task<ResultService<List<RentalDashDto>>> GetAllDash()
        {
            var rentals = await _repo.GetAllRentalsDash();
            if (rentals.Count < 1) return ResultService.Fail<List<RentalDashDto>>("Não foram encontrados alguéis.");
            var rentalsDashDto = _mapper.Map<List<RentalDashDto>>(rentals);
            return ResultService.Ok(rentalsDashDto);
        }

        public async Task<ResultService<RentalDto>> GetById(int id)
        {
            var rental = await _repo.GetRentalById(id);
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

            var book = await _bookRepo.GetBookById(rental.BookId);
            if (book == null)
                return ResultService.Fail<CreateRentalDto>("Livro não encontrado!");

            var user = await _userRepo.GetUserById(rental.UserId);
            if (user == null)
                return ResultService.Fail<CreateRentalDto>("Usuário não encontrado!");

            if (rental.RentalDate.Date != DateTime.Now.Date)
                return ResultService.Fail<CreateRentalDto>("Data de aluguel não pode ser diferente da data de Hoje!");

            bool? forecastValidate = await _repo.CheckForecastDate(rental.ForecastDate, rental.RentalDate);
            if (forecastValidate == true)
                return ResultService.Fail<UpdateRentalDto>("Prazo do aluguel não pode ser superior a 30 dias!");
            else if (forecastValidate == false)
                return ResultService.Fail<UpdateRentalDto>("Data de Previsão não pode ser anterior à Data do Aluguel!");

            var userRental = await _repo.GetRentalByUserIdandBookId(book.Id, user.Id);
            if (userRental.Count > 0)
                return ResultService.Fail<UpdateRentalDto>("Usuário já possui aluguel desse livro!");

            var updateBook = await _bookRepo.UpdateQuantity(book.Id, false);
            if (updateBook == false)
                return ResultService.Fail<UpdateRentalDto>("Livro com estoque esgotado.");

            rental.Status = "Pendente";

            await _repo.Add(rental);

            return ResultService.Ok("Aluguel adicionado com êxito.");
        }

        public async Task<ResultService> Update(UpdateRentalDto model)
        {
            var result = await _repo.GetRentalById((int)model.Id);
            if (result == null)
                return ResultService.Fail<UpdateRentalDto>("Aluguel não encontrado!");

            var rental = _mapper.Map(model, result);

            var validation = new UpdateRentalDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            if (rental.ReturnDate.Value.Date != DateTime.Now.Date)
                return ResultService.Fail<CreateRentalDto>("Data de devolução não pode ser diferente da data de Hoje!");

            rental.Status = await _repo.GetStatus(rental.ForecastDate, rental.ReturnDate);
            
            await _repo.Update(rental);

            bool updateBook = await _bookRepo.UpdateQuantity(rental.BookId, true);
            if (updateBook == false)
                return ResultService.Fail<UpdateRentalDto>("Livro com estoque esgotado.");

            return ResultService.Ok("Devolução realizada com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var rental = await _repo.GetRentalById(id);
            if (rental == null)
                return ResultService.Fail<RentalDto>("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.Fail<RentalDto>("Aluguel foi devolvido, não pode ser deletado.");

            await _repo.Delete(rental);

            return ResultService.Ok("Aluguel deletado com êxito!");
        }
    }
}
