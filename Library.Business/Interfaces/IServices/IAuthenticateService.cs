using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface IAuthenticateService
    {
        Task<ResultService> Authenticate(string email, string password);
        public string GenerateToken(int id, string email);

    }
}
