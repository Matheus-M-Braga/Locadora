using AutoMapper;
using Library.Business.Interfaces.IRepository;
using Library.Business.Models;
using Library.Business.Models.Dtos.User;
using Library.Business.Pagination;
using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos;
using Library.Business.Models.Dtos.Validations;

namespace Library.Business.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public UsersService(IUserRepository repo, IRentalRepository rentalRepo, IMapper mapper)
        {
            _userRepository = repo;
            _rentalRepository = rentalRepo;
            _mapper = mapper;
        }

        public async Task<ResultService<List<Users>>> GetAll(FilterDb filterDb)
        {
            var users = await _userRepository.GetAllUsersPaged(filterDb);
            var result = new PagedBaseResponseDto<Users>(users.TotalRegisters, users.TotalPages, users.PageNumber, _mapper.Map<List<Users>>(users.Data));

            if (result.Data.Count == 0) return ResultService.NotFound<List<Users>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters,result.PageNumber, result.TotalPages);
        }

        public async Task<ResultService<List<UserRentalDto>>> GetAllSelect()
        {
            var users = await _userRepository.GetAllUsers();
            return ResultService.Ok(_mapper.Map<List<UserRentalDto>>(users));
        }

        public async Task<ResultService<Users>> GetById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return ResultService.NotFound<Users>("Usuário não encontrado!");

            return ResultService.Ok(_mapper.Map<Users>(user));
        }

        public async Task<ResultService> Create(CreateUserDto model)
        {
            var validation = new UserDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            var emailExists = await _userRepository.GetUserByEmail(model.Email);
            if (emailExists.Count > 0) return ResultService.BadRequest("Email já cadastrado.");

            var user = _mapper.Map<Users>(model);
            await _userRepository.Add(user);

            return ResultService.Created("Usuário adicionado com êxito.");
        }

        public async Task<ResultService> Update(UpdateUserDto model)
        {
            var user = _mapper.Map<Users>(model);

            var result = await _userRepository.GetUserById(user.Id);
            if (result == null) return ResultService.NotFound("Usuário não encontrado!");

            var validation = new UpdateUserDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            if(result.Email != model.Email)
            {
                var emailExists = await _userRepository.GetUserByEmail(model.Email);
                if (emailExists.Count > 0) return ResultService.BadRequest("Email já cadastrado.");
            }

            await _userRepository.Update(user);

            return ResultService.Ok("Usuário atualizado com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return ResultService.NotFound<Users>("Usuário não encontrado!");

            var rentalAssociation = await _rentalRepository.GetAllRentalsByUserId(id);

            if (rentalAssociation.Count > 0) return ResultService.BadRequest("Possui associação com aluguéis.");

            await _userRepository.Delete(user);

            return ResultService.Ok("Usuário deletado com êxito!");
        }
    }
}
