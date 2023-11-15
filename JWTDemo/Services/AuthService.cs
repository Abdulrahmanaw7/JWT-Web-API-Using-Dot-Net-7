using JWTDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTDemo.Services
{
    /* The `AuthService` class is implementing the `IAuthService` interface. It is a service class
    responsible for handling authentication and authorization logic in the application. It provides
    methods for assigning user roles, generating JWT tokens, and registering new users. */
    public class AuthService : IAuthService
    {
        /* The line `private readonly UserManager<ApplicationUser> _userManager;` is declaring a private
        readonly field `_userManager` of type `UserManager<ApplicationUser>`. */
        private readonly UserManager<ApplicationUser> _userManager;
        /* The line `private readonly RoleManager<IdentityRole> _roleManager;` is declaring a private
        readonly field `_roleManager` of type `RoleManager<IdentityRole>`. This field is used to manage
        user roles in the application. It allows for creating, deleting, and managing roles for users. */
        private readonly RoleManager<IdentityRole> _roleManager;
        /* The line `private readonly JWT _jwt;` is declaring a private readonly field `_jwt` of type
        `JWT`. This field is used to store the JWT configuration options, such as the key, issuer,
        audience, and duration. It is injected into the `AuthService` class through the constructor
        using the `IOptions<JWT>` interface. */
        private readonly JWT _jwt;
        /* The `AuthService` constructor is initializing the private fields `_userManager`, `_roleManager`, and
        `_jwt` with the provided dependencies. */
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jWT)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jWT.Value;
        }

        /// <summary>
        /// The function assigns a role to a user asynchronously and returns a string indicating the
        /// success or failure of the operation.
        /// </summary>
        /// <param name="AssignUserRoleModel">A model that contains the necessary information to assign a
        /// user to a role. It typically includes properties like UserId (the ID of the user to be
        /// assigned) and Role (the name of the role to assign the user to).</param>
        /// <returns>
        /// The method returns a string. If the user or role is invalid, it returns "The user or role
        /// invalid". If the user is already assigned to the role, it returns "user already assign to this
        /// role". If the assignment is successful, it returns an empty string. If there is an error
        /// during the assignment, it returns "something wrong".
        /// </returns>
        public async Task<string> AssignUserRoleAsync(AssignUserRoleModel assignUserRoleModel)
        {
            var user = await _userManager.FindByIdAsync(assignUserRoleModel.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(assignUserRoleModel.Role))
            {
                return "The user or role invalid";
            }
            if (await _userManager.IsInRoleAsync(user, assignUserRoleModel.Role))
            {
                return "user already assign to this role";
            }
            var result = await _userManager.AddToRoleAsync(user, assignUserRoleModel.Role);

            return result.Succeeded ? string.Empty : "somthing worning ";
        }

        /// <summary>
        /// The function `GetTokenAsync` takes in a `TokenRequestModel` and returns an `AuthModel`
        /// containing authentication information for the user.
        /// </summary>
        /// <param name="TokenRequestModel">The TokenRequestModel is a model that contains the following
        /// properties:</param>
        /// <returns>
        /// The method is returning an instance of the `AuthModel` class.
        /// </returns>
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel tokenRequestModel)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByNameAsync(tokenRequestModel.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, tokenRequestModel.Password))
            {
                authModel.Message = "UserName OR Password inCorrect";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var userRoles = await _userManager.GetRolesAsync(user);


            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Username = user.UserName;
            authModel.Roles = userRoles.ToList();

            return authModel;
        }

        /// <summary>
        /// The function `RegisterAsync` registers a new user by checking if the email and username are
        /// already registered, creating a new user if not, and returning an authentication model with a
        /// JWT token.
        /// </summary>
        /// <param name="RegisterModel">The RegisterModel is a model that contains the following
        /// properties:</param>
        /// <returns>
        /// The method returns an instance of the AuthModel class.
        /// </returns>
        public async Task<AuthModel> RegisterAsync(RegisterModel registerModel)
        {
            if (await _userManager.FindByEmailAsync(registerModel.Email) != null)
                return new AuthModel { Message = "Email is alrady register!" };
            if (await _userManager.FindByNameAsync(registerModel.UserName) != null)
                return new AuthModel { Message = "UserName is alrady register!" };

            var user = new ApplicationUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName
            };
            var result = await _userManager.CreateAsync(user, registerModel.password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {

                    errors += $"{error.Description},";

                }
                return new AuthModel { Message = errors };

            }
            await _userManager.AddToRoleAsync(user, "user");
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                Username = user.UserName,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }
        /// <summary>
        /// The function creates a JWT token for a given user with their claims and roles.
        /// </summary>
        /// <param name="ApplicationUser">The `ApplicationUser` parameter is an object representing the
        /// user for whom the JWT token is being created. It typically contains information such as the
        /// user's username, email, and ID.</param>
        /// <returns>
        /// The method is returning a Task of JwtSecurityToken.
        /// </returns>
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}