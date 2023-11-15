using JWTDemo.Model;
using JWTDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace JWTDemo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
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

