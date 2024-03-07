using Library.Business.Interfaces.IRepository;
using Library.Business.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Library.Business.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly ILoginUserRepository _loginUserRepository;
        private readonly IConfiguration _configuration;

        public AuthenticateService(ILoginUserRepository loginUserRepository, IConfiguration configuration)
        {
            _loginUserRepository = loginUserRepository;
            _configuration = configuration;
        }

        public async Task<ResultService> Authenticate(string email, string password)
        {
            var loginUser = _loginUserRepository.Search(lu => lu.Email.ToLower() == email.ToLower()).Result.FirstOrDefault();

            if (loginUser == null) return ResultService.BadRequest("Usuário não existe.");

            using var hmac = new HMACSHA512(loginUser.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int x = 0; x < computedHash.Length; x++)
            {
                if (computedHash[x] != loginUser.PasswordHash[x]) return ResultService.BadRequest("Email ou senha inválidos.");
            }

            var token = GenerateToken(loginUser.Id, loginUser.Email);

            return ResultService.Ok(token);
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(4);

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
