using AutoMapper;
using Locadora.API.Models;
using Locadora.API.Dtos;
using Locadora.API.Dtos.Validations;
using Locadora.API.Services.Interfaces;
using Locadora.API.Repository;

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

        public async Task<ResultService<ICollection<RentalsDto>>> GetAsync()
        {
            var rentals = await _repo.GetAllRentals(true, true);
            return ResultService.Ok(_mapper.Map<ICollection<RentalsDto>>(rentals));
        }

        public async Task<ResultService<RentalsDto>> GetByIdAsync(int id)
        {
            var rental = await _repo.GetRentalById(id, true, true);
            if (rental == null)
                return ResultService.Fail<RentalsDto>("Aluguel não encontrado!");

            return ResultService.Ok(_mapper.Map<RentalsDto>(rental));
        }

        public async Task<ResultService> CreateAsync(CreateRentalDto model)
        {
            if (model == null)
                return ResultService.Fail<CreateRentalDto>("Objeto deve ser informado!");

            var result = new RentalDtoValidator().Validate(model);
            if (!result.IsValid)
                return ResultService.RequestError<CreateRentalDto>("Problemas de validação", result);

            var book = await _bookRepo.GetBookById(model.BookId, false);
            if (book == null)
                return ResultService.Fail<CreateRentalDto>("Livro não encontrado!");

            var user = await _userRepo.GetUserById(model.UserId);
            if (user == null)
                return ResultService.Fail<CreateRentalDto>("Usuário não encontrado!");

            bool dateValidate = await _repo.CheckDate(model.RentalDate);
            if (dateValidate)
                return ResultService.Fail<CreateRentalDto>("Data de aluguel não pode ser diferente da data de Hoje!");

            var userRental = await _repo.GetRentalByUserIdandBookId(book.Id, user.Id);
            if(userRental.Count > 0) 
                return ResultService.Fail<RentalReturnDto>("Usuário já possui aluguel desse livro!");

            var updateBook = await _bookRepo.UpdateQuantity(book.Id, false);
            if (updateBook == false)
                return ResultService.Fail<RentalReturnDto>("Livro com estoque esgotado.");

            var rental = _mapper.Map<Rentals>(model);
            rental.Status = "Pendente";
            await _repo.Add(rental);
            await _repo.SaveChanges();

            var created = await _repo.GetRentalById(rental.Id, true, true);

            return ResultService.Ok(_mapper.Map<RentalsDto>(created));
        }

        public async Task<ResultService> UpdateAsync(RentalReturnDto model)
        {
            if (model == null)
                return ResultService.Fail<RentalReturnDto>("Objeto deve ser informado!");

            var validation = new UpdateRentalDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError<RentalReturnDto>("Problemas de validação", validation);

            var rental = await _repo.GetRentalById(model.Id, true, true);
            if (rental == null)
                return ResultService.Fail<RentalReturnDto>("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.Fail<RentalReturnDto>("Aluguel já devolvido!");

            var book = await _bookRepo.GetBookById(rental.BookId);
            if (book == null)
                return ResultService.Fail<RentalReturnDto>("Livro não encontrado!");

            var user = await _userRepo.GetUserById(rental.UserId);
            if (user == null)
                return ResultService.Fail<RentalReturnDto>("Usuário não encontrado!");

            bool dateValidate = await _repo.CheckDate(model.ReturnDate);
            if (dateValidate)
                return ResultService.Fail<CreateRentalDto>("Data de devolução não pode ser diferente da data de Hoje!");

            bool status = await _repo.GetStatus(rental.ForecastDate, rental.ReturnDate);
            if(status)
                rental.Status = "No prazo";
            else 
                rental.Status = "Atrasado";

            bool updateBook = await _bookRepo.UpdateQuantity(rental.BookId, true);
            if (updateBook == false)
                return ResultService.Fail<RentalReturnDto>("Livro com estoque esgotado.");

            rental = _mapper.Map(model, rental);
            await _repo.Update(rental);
            await _repo.SaveChanges();

            return ResultService.Ok(_mapper.Map<RentalsDto>(rental));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var rental = await _repo.GetRentalById(id);
            if (rental == null)
                return ResultService.Fail<RentalsDto>("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.Fail<RentalsDto>("Aluguel foi devolvido, não pode ser deletado.");

            await _repo.Delete(rental);
            await _repo.SaveChanges();

            return ResultService.Ok("Aluguel deletado com sucesso");
        }
    }
}
