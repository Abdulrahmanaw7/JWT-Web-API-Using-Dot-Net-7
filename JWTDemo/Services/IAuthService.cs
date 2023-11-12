using JWTDemo.Model;

namespace JWTDemo.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);
    }
}
