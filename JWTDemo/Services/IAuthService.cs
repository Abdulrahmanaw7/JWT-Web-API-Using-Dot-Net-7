using JWTDemo.Model;

namespace JWTDemo.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);
        Task<AuthModel> GetTokenAsync(TokenRequestModel tokenRequestModel);
        Task<string> AssignUserRoleAsync(AssignUserRoleModel assignUserRoleModel);

    }
}
