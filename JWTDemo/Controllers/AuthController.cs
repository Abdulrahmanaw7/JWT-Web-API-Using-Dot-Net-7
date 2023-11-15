using JWTDemo.Model;
using JWTDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace JWTDemo.Controllers
{
    [Route("api/[controller]")]
 /* The `AuthController` class is a controller in an ASP.NET Core web application. It inherits from the
 `ControllerBase` class, which provides basic functionality for handling HTTP requests and
 generating HTTP responses. The `AuthController` is responsible for handling authentication-related
 requests, such as user registration, token generation, and assigning user roles. */
    public class AuthController : ControllerBase
    {
      /* The line `private readonly IAuthService _authService;` is declaring a private readonly field
      `_authService` of type `IAuthService`. This field is used to store an instance of a class that
      implements the `IAuthService` interface. The `IAuthService` interface likely defines methods
      and properties related to authentication functionality, and the `_authService` field allows
      the `AuthController` class to access and use these authentication services. */
        private readonly IAuthService _authService;
       /* The `public AuthController(IAuthService authService)` is a constructor for the
       `AuthController` class. It takes an argument of type `IAuthService` named `authService`. */
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// The function is a HTTP POST endpoint for user registration that returns a token, expiration
        /// date, and user role if the registration is successful.
        /// </summary>
        /// <param name="RegisterModel">The RegisterModel is a model class that contains the data
        /// required for user registration. It typically includes properties such as username, email,
        /// password, and any other relevant information needed to create a new user account.</param>
        /// <returns>
        /// The method is returning an IActionResult. If the ModelState is not valid, it will return a
        /// BadRequest with the ModelState. If the registration is successful, it will return an Ok
        /// response with a token, expiration date, and user role. If the registration is not
        /// successful, it will return a BadRequest with the error message.
        /// </returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { token = result.Token, expiresOn = result.ExpiresOn, userRole = result.Roles });
        }

       /// <summary>
       /// This function handles a POST request to generate a token based on a given token request
       /// model.
       /// </summary>
       /// <param name="TokenRequestModel">The TokenRequestModel is a model class that represents the
       /// request body for the GetTokenAsync method. It contains the necessary properties to retrieve a
       /// token, such as username and password.</param>
       /// <returns>
       /// The method is returning an IActionResult.
       /// </returns>
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
       
        /// <summary>
        /// This function handles a POST request to assign a user role and returns the assigned role if
        /// successful.
        /// </summary>
        /// <param name="AssignUserRoleModel">AssignUserRoleModel is a model class that is used to pass
        /// the necessary data for assigning a user role. It contains properties that represent the
        /// required information for the assignment, such as the user ID and the role ID.</param>
        /// <returns>
        /// The method is returning an IActionResult. If the ModelState is not valid, it will return a
        /// BadRequest with the ModelState. If the result of assigning the user role is not empty, it
        /// will return a BadRequest with the result. Otherwise, it will return an Ok with the model.
        /// </returns>
        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignUserRoleAsync([FromBody] AssignUserRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AssignUserRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            // in succses will retuern to mobile and front end
            return Ok(model);
        }
    }

}

