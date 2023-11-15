using JWTDemo.Model;

namespace JWTDemo.Services
{
/* The `public interface IAuthService` is defining an interface called `IAuthService`. This interface
specifies the contract for a service that handles authentication-related operations. It declares
several methods that can be implemented by classes that implement this interface. These methods
include `RegisterAsync`, `GetTokenAsync`, and `AssignUserRoleAsync`, which are responsible for user
registration, token retrieval, and assigning user roles, respectively. */
    public interface IAuthService
    {
        /// <summary>
        /// The function "RegisterAsync" asynchronously registers a user with the provided
        /// registerModel.
        /// </summary>
        /// <param name="RegisterModel">A model that contains the necessary information for user
        /// registration, such as username, email, and password.</param>
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);
       /* The `GetTokenAsync` method is a function that asynchronously retrieves a token for
       authentication. It takes a `TokenRequestModel` object as a parameter, which contains the
       necessary information for requesting a token. The method returns a `Task<AuthModel>` object,
       which represents the asynchronous operation of retrieving the token. The `AuthModel` object
       contains the token and other relevant information related to authentication. */
        Task<AuthModel> GetTokenAsync(TokenRequestModel tokenRequestModel);
        /// <summary>
        /// The function AssignUserRoleAsync assigns a user role to a user asynchronously.
        /// </summary>
        /// <param name="AssignUserRoleModel">A model that contains the necessary information to assign
        /// a user to a specific role.</param>
        Task<string> AssignUserRoleAsync(AssignUserRoleModel assignUserRoleModel);

    }
}
