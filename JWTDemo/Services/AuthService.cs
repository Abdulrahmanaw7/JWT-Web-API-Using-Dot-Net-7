using JWTDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTDemo.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser>  userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jWT)
        {
            _userManager = userManager;     
            _roleManager = roleManager;
            _jwt = jWT.Value;
        }

        public async Task<string> AssignUserRoleAsync(AssignUserRoleModel assignUserRoleModel) 
        {
            var user = await _userManager.FindByIdAsync(assignUserRoleModel.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(assignUserRoleModel.Role))
            {
                return "The user or role invalid";
            }
            if (await _userManager.IsInRoleAsync(user,assignUserRoleModel.Role))
            {
                return "user already assign to this role";
            }
            var result= await _userManager.AddToRoleAsync(user, assignUserRoleModel.Role);

            return  result.Succeeded?string.Empty:"somthing worning ";
        }

        public  async Task<AuthModel> GetTokenAsync(TokenRequestModel tokenRequestModel)
        {
           var authModel= new AuthModel();
            var user = await _userManager.FindByNameAsync(tokenRequestModel.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, tokenRequestModel.Password))
            {
                authModel.Message = "UserName OR Password inCorrect";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var userRoles = await _userManager.GetRolesAsync(user);


            authModel.IsAuthenticated= true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Username= user.UserName;
            authModel.Roles = userRoles.ToList();

            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel registerModel)
        {
            if (await _userManager.FindByEmailAsync(registerModel.Email)!=null) 
                return new AuthModel { Message = "Email is alrady register!"};
            if (await _userManager.FindByNameAsync(registerModel.UserName) != null)
                return new AuthModel { Message = "UserName is alrady register!" };

            var user= new ApplicationUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName
            };
            var result =await _userManager.CreateAsync(user, registerModel.password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {

                    errors +=$"{ error.Description},";

                }
                return new AuthModel { Message=errors};

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