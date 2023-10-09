#pragma warning disable CS8604
using AutoMapper;
using Locadora.API.Models;
using Locadora.API.Dtos;
using Locadora.API.Dtos.Validations;
using Locadora.API.Services.Interfaces;
using Locadora.API.Repository;
using Locadora.API.Repository.Pagination;

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

        public async Task<ResultService<PagedBaseResponseDto<RentalsDto>>> GetAll(FilterDb filterDb)
        {
            var rentals = await _repo.GetAllRentals(filterDb);
            var result = new PagedBaseResponseDto<RentalsDto>(rentals.TotalRegisters, rentals.TotalPages, _mapper.Map<List<RentalsDto>>(rentals.Data));

            if(result.Data.Count == 0)
                return ResultService.Fail<PagedBaseResponseDto<RentalsDto>>("Nenhum registro encontrado.");

            return ResultService.Ok(result);
        }

        public async Task<ResultService<RentalsDto>> GetById(int id)
        {
            var rental = await _repo.GetRentalById(id);
            if (rental == null)
                return ResultService.Fail<RentalsDto>("Aluguel não encontrado!");

            return ResultService.Ok(_mapper.Map<RentalsDto>(rental));
        }

        public async Task<ResultService> Create(CreateRentalDto model)
        {
            if (model == null)
                return ResultService.Fail<CreateRentalDto>("Objeto deve ser informado!");

            var result = new RentalDtoValidator().Validate(model);
            if (!result.IsValid)
                return ResultService.RequestError<CreateRentalDto>("Problmeas", result);

            var book = await _bookRepo.GetBookById(model.BookId);
            if (book == null)
                return ResultService.Fail<CreateRentalDto>("Livro não encontrado!");

            var user = await _userRepo.GetUserById(model.UserId);
            if (user == null)
                return ResultService.Fail<CreateRentalDto>("Usuário não encontrado!");

            bool dateValidate = await _repo.CheckDate(model.RentalDate);
            if (dateValidate)
                return ResultService.Fail<CreateRentalDto>("Data de aluguel não pode ser diferente da data de Hoje!");

            bool? forecastValidate = await _repo.CheckForecastDate(model.ForecastDate, model.RentalDate);
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

            var rental = _mapper.Map<Rentals>(model);
            rental.Status = "Pendente";

            await _repo.Add(rental);

            return ResultService.Ok("Aluguel adicionado com êxito.");
        }

        public async Task<ResultService> Update(UpdateRentalDto model)
        {
            if (model == null)
                return ResultService.Fail<UpdateRentalDto>("Objeto deve ser informado!");

            var validation = new UpdateRentalDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError<UpdateRentalDto>("Problmeas", validation);

            var rental = await _repo.GetRentalById(model.Id);
            if (rental == null)
                return ResultService.Fail<UpdateRentalDto>("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.Fail<UpdateRentalDto>("Aluguel já devolvido!");

            var book = await _bookRepo.GetBookById(rental.BookId);
            if (book == null)
                return ResultService.Fail<UpdateRentalDto>("Livro não encontrado!");

            var user = await _userRepo.GetUserById(rental.UserId);
            if (user == null)
                return ResultService.Fail<UpdateRentalDto>("Usuário não encontrado!");

            bool dateValidate = await _repo.CheckDate(model.ReturnDate);
            if (dateValidate)
                return ResultService.Fail<CreateRentalDto>("Data de devolução não pode ser diferente da data de Hoje!");

            bool status = await _repo.GetStatus(rental.ForecastDate, rental.ReturnDate);
            if (status)
                rental.Status = "No prazo";
            else
                rental.Status = "Atrasado";

            rental = _mapper.Map(model, rental);
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
                return ResultService.Fail<RentalsDto>("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.Fail<RentalsDto>("Aluguel foi devolvido, não pode ser deletado.");

            await _repo.Delete(rental);

            return ResultService.Ok("Aluguel deletado com êxito!");
        }
    }
}
