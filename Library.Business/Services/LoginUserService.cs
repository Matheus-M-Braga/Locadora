using AutoMapper;
using Library.Business.Interfaces.IRepository;
using Library.Business.Interfaces.IServices;
using Library.Business.Models;
using Library.Business.Models.Dtos;
using Library.Business.Models.Dtos.LoginUser;
using Library.Business.Models.Dtos.Validations;
using Library.Business.Pagination;
using System.Security.Cryptography;
using System.Text;

namespace Library.Business.Services
{
    public class LoginUserService : ILoginUserService
    {
        private readonly ILoginUserRepository _loginUserRepository;
        private readonly IMapper _mapper;
        public LoginUserService(ILoginUserRepository loginUserRepository, IMapper mapper)
        {
            _loginUserRepository = loginUserRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<List<LoginUserDto>>> GetAll(PagedBaseRequest request)
        {
            var loginUsers = await _loginUserRepository.GetAll(request);
            var result = new PagedBaseResponseDto<LoginUserDto>(loginUsers.TotalRegisters, loginUsers.TotalPages, request.PageNumber, _mapper.Map<List<LoginUserDto>>(loginUsers.Data));
            if (result.Data.Count == 0) return ResultService.NotFound<List<LoginUserDto>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.PageNumber, result.TotalPages);
        }

        public async Task<ResultService<LoginUserDto>> GetById(int id)
        {
            var loginUser = await _loginUserRepository.GetById(id);
            if (loginUser == null) return ResultService.NotFound<LoginUserDto>("Usuário não encontrado.");

            return ResultService.Ok(_mapper.Map<LoginUserDto>(loginUser));
        }

        public async Task<ResultService> Add(LoginUserCreateDto model)
        {
            var validation = new LoginUserCreateDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            if (_loginUserRepository.Search(lu => lu.Email.ToLower() == model.Email.ToLower()).Result.Any()) return ResultService.BadRequest("Email já cadastrado.");

            var loginUser = _mapper.Map<LoginUsers>(model);
            using var hmac = new HMACSHA512();
            loginUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            loginUser.PasswordSalt = hmac.Key;

            await _loginUserRepository.Add(loginUser);
            return ResultService.Created("Usuário adicionado com êxito.");
        }

        public async Task<ResultService> Update(LoginUserUpdateDto model)
        {
            if (!_loginUserRepository.Search(lu => lu.Id == model.Id).Result.Any()) return ResultService.NotFound("Usuário não encontrado");

            var validation = new LoginUserUpdateDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            var loginUser = _mapper.Map<LoginUsers>(model);

            using var hmac = new HMACSHA512();
            loginUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            loginUser.PasswordSalt = hmac.Key;

            await _loginUserRepository.Update(loginUser);
            return ResultService.Ok("Usuário atualizado com êxito.");
        }

        public async Task<ResultService> Delete(int id)
        {
            if (!_loginUserRepository.Search(lu => lu.Id == id).Result.Any()) return ResultService.NotFound("Usuário não encontrado");

            await _loginUserRepository.Delete(id);
            return ResultService.Ok("Usuário deletado com êxito.");
        }
    }
}
