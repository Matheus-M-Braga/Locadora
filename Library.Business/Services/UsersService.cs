using AutoMapper;
using Library.Business.Interfaces.IRepository;
using Library.Business.Interfaces.IServices;
using Library.Business.Models;
using Library.Business.Models.Dtos;
using Library.Business.Models.Dtos.User;
using Library.Business.Models.Dtos.Validations;
using Library.Business.Pagination;

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

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.PageNumber, result.TotalPages);
        }

        public async Task<ResultService<List<UserListDto>>> GetSummary()
        {
            var users = await _userRepository.GetSummary();
            return ResultService.Ok(_mapper.Map<List<UserListDto>>(users));
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

            if (_userRepository.Search(u => u.Email == model.Email).Result.Any()) return ResultService.BadRequest("Email já cadastrado.");

            await _userRepository.Add(_mapper.Map<Users>(model));
            return ResultService.Created("Usuário adicionado com êxito.");
        }

        public async Task<ResultService> Update(UpdateUserDto model)
        {
            if (!_userRepository.Search(u => u.Id == model.Id).Result.Any()) return ResultService.NotFound("Usuário não encontrado!");

            var validation = new UpdateUserDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            if (_userRepository.Search(u => u.Email == model.Email && u.Id != model.Id).Result.Any()) return ResultService.BadRequest("Email já cadastrado.");

            await _userRepository.Update(_mapper.Map<Users>(model));
            return ResultService.Ok("Usuário atualizado com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            if (!_userRepository.Search(u => u.Id == id).Result.Any()) return ResultService.NotFound<Users>("Usuário não encontrado!");

            if (_rentalRepository.Search(r => r.UserId == id).Result.Any()) return ResultService.BadRequest("Possui associação com aluguéis.");

            await _userRepository.Delete(id);
            return ResultService.Ok("Usuário deletado com êxito!");
        }
    }
}
