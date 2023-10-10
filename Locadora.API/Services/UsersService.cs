using AutoMapper;
using System.Xml.Linq;
using Locadora.API.Models;
using Locadora.API.Context;
using Locadora.API.Dtos;
using Locadora.API.Dtos.Validations;
using Locadora.API.Services.Interfaces;
using Locadora.API.Pagination;
using Locadora.API.Repository.Interfaces;

namespace Locadora.API.Services {
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _repo;
        private readonly IRentalRepository _rentalRepo;
        private readonly IMapper _mapper;

        public UsersService(IUserRepository repo, IRentalRepository rentalRepo, IMapper mapper)
        {
            _repo = repo;
            _rentalRepo = rentalRepo;
            _mapper = mapper;
        }

        public async Task<ResultService<PagedBaseResponseDto<Users>>> GetAll(FilterDb filterDb)
        {
            var users = await _repo.GetAllUsersPaged(filterDb);
            var result = new PagedBaseResponseDto<Users>(users.TotalRegisters, users.TotalPages, _mapper.Map<List<Users>>(users.Data));

            if (result.Data.Count == 0)
                return ResultService.Fail<PagedBaseResponseDto<Users>>("Nenhum registro encontrado.");

            return ResultService.Ok(result);
        }

        public async Task<ResultService<Users>> GetById(int id)
        {
            var user = await _repo.GetUserById(id);
            if (user == null)
                return ResultService.Fail<Users>("Usuário não encontrado!");

            return ResultService.Ok(_mapper.Map<Users>(user));
        }

        public async Task<ResultService<ICollection<UserRentalDto>>> GetAllSelect()
        {
            var users = await _repo.GetAllUsers();
            return ResultService.Ok(_mapper.Map<ICollection<UserRentalDto>>(users));
        }

        public async Task<ResultService> Create(CreateUserDto model)
        {
            if (model == null)
                return ResultService.Fail<CreateUserDto>("Objeto deve ser informado!");

            var result = new UserDtoValidator().Validate(model);
            if (!result.IsValid)
                return ResultService.RequestError<CreateUserDto>("Problmeas", result);

            var emailExists = await _repo.GetUserByEmail(model.Email);
            if (emailExists.Count > 0)
                return ResultService.Fail<Users>("Email já cadastrado.");

            var user = _mapper.Map<Users>(model);
            await _repo.Add(user);

            return ResultService.Ok("Usuário adicionado com êxito.");
        }

        public async Task<ResultService> Update(Users model)
        {
            if (model == null)
                return ResultService.Fail<Users>("Objeto deve ser informado!");

            var validation = new UpdateUserDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError<Users>("Problmeas", validation);

            var user = await _repo.GetUserById(model.Id);
            if (user == null)
                return ResultService.Fail("Usuário não encontrado!");

            user = _mapper.Map(model, user);
            await _repo.Update(user);

            return ResultService.Ok("Usuário atualizado com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var user = await _repo.GetUserById(id);
            if (user == null)
                return ResultService.Fail<Users>("Usuário não encontrado!");

            var rentalAssociation = await _rentalRepo.GetAllRentalsByUserId(id);

            if (rentalAssociation.Count > 0)
                return ResultService.Fail<Publishers>("Erro ao excluir usuário: Possui associação com aluguéis.");

            await _repo.Delete(user);

            return ResultService.Ok("Usuário deletado com êxito!");
        }
    }
}
