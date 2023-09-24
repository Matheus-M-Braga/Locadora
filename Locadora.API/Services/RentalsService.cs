using AutoMapper;
using Locadora.API.Models;
using Locadora.API.Dtos;
using Locadora.API.Dtos.Validations;
using Locadora.API.Services.Interface;
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

            bool rentalDateValidate = await _repo.CheckRentalDate(model.RentalDate);
            if (rentalDateValidate == true)
                return ResultService.Fail<CreateRentalDto>("Data de aluguel não pode ser diferente Hoje!");


            var rental = _mapper.Map<Rentals>(model);

            await _repo.Add(rental);
            await _repo.SaveChanges();

            // await _repo.UpdateBookQuantityAsync(true, book.Id);

            return ResultService.Ok(rental);
        }

        public async Task<ResultService> UpdateAsync(RentalReturnDto model)
        {

            if (model == null)
                return ResultService.Fail<RentalReturnDto>("Objeto deve ser informado!");

            var rentalValidate = _mapper.Map<CreateRentalDto>(model);
            var validation = new RentalDtoValidator().Validate(rentalValidate);
            if (!validation.IsValid)
                return ResultService.RequestError<RentalReturnDto>("Problemas de validação", validation);

            var rental = await _repo.GetRentalById(model.Id, true, true);
            if (rental == null)
                return ResultService.Fail<RentalReturnDto>("Aluguel não encontrado!");

            rental = _mapper.Map(model, rental);
            var book = await _bookRepo.GetBookById(rental.BookId);
            if (book == null)
                return ResultService.Fail<RentalReturnDto>("Livro não encontrado!");

            var user = await _userRepo.GetUserById(rental.UserId);
            if (user == null)
                return ResultService.Fail<RentalReturnDto>("Usuário não encontrado!");

            //await _repo.UpdateBookQuantityAsync(false, book.Id);

            await _repo.Update(rental);

            return ResultService.Ok(_mapper.Map<RentalsDto>(rental));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var rental = await _repo.GetRentalById(id);
            if (rental == null)
                return ResultService.Fail<RentalsDto>("Aluguel não encontrado!");

            await _repo.Delete(rental);
            return ResultService.Ok("Aluguel deletado com sucesso");
        }
    }
}
