using AutoMapper;
using Library.Business.Interfaces.IRepository;
using Library.Business.Interfaces.IServices;
using Library.Business.Models;
using Library.Business.Models.Dtos.LoginUser;
using Library.Business.Models.Dtos.Validations;
using System.Security.Cryptography;
using System.Text;

namespace Library.Business.Services
{
    public class LoginUserService : ILoginUserService
    {
        private readonly ILoginUserRepository _loginUserRepository;
        private readonly IAuthenticateService _authenticate;
        private readonly IMapper _mapper;
        public LoginUserService(ILoginUserRepository loginUserRepository, IMapper mapper, IAuthenticateService authenticate)
        {
            _loginUserRepository = loginUserRepository;
            _mapper = mapper;
            _authenticate = authenticate;
        }

        public async Task<ResultService<List<LoginUser>>> GetAll()
        {
            var loginUsers = await _loginUserRepository.GetAll();

            if (loginUsers.Count == 0) return ResultService.NotFound<List<LoginUser>>("Nenhum registro encontrado.");

            return ResultService.Ok(loginUsers);
        }

        public async Task<ResultService<LoginUser>> GetById(int id)
        {
            var loginUser = await _loginUserRepository.GetById(id);

            if (loginUser == null) return ResultService.NotFound<LoginUser>("Usuário não encontrado.");

            return ResultService.Ok(loginUser);
        }

        public async Task<ResultService> Add(LoginUserCreateDto model)
        {
            var validation = new LoginUserCreateDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            var emailExists = await _loginUserRepository.GetLoginUserByEmail(model.Email);
            if (emailExists != null) return ResultService.BadRequest("Email já cadastrado.");

            var loginUser = _mapper.Map<LoginUser>(model);
            using var hmac = new HMACSHA512();
            loginUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            loginUser.PasswordSalt = hmac.Key;

            await _loginUserRepository.Add(loginUser);
            return ResultService.Created("Usuário adicionado com êxito.");
        }

        public async Task<ResultService> Update(LoginUserUpdateDto model)
        {
            var validation = new LoginUserUpdateDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            await _loginUserRepository.Update(_mapper.Map<LoginUser>(model));
            return ResultService.Ok("Usuário atualizado com êxito.");
        }

        public async Task<ResultService> Delete(int id)
        {
            var loginUser = await _loginUserRepository.GetById(id);
            if (loginUser == null) return ResultService.NotFound("Usuário não encontrado.");

            await _loginUserRepository.Delete(loginUser);
            return ResultService.Ok("Usuário deletado com êxito.");
        }
    }
}
