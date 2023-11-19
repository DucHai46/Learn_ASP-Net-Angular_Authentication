using API_identity.DataTransferObjects;
using API_identity.Entities.Models;
using API_identity.JwtFeatures;
using API_identity.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API_identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly AuthService _authService;

        public AccountController(UserManager<User> userManager, AuthService _authService)
        {
            this.userManager = userManager;
            this._authService = _authService;
        }


        [HttpPost("Registration")]
        public IActionResult RegisterUser([FromBody] UserRegistertrationDto registertrationDto)
        {
            if (registertrationDto == null || !ModelState.IsValid)
            {
                return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = false });
            }
            return Ok(this._authService.RegistrationResponseDto(registertrationDto));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserAuthentication userAuthentication)
        {
            var user = await userManager.FindByNameAsync(userAuthentication.Email);

            if(user == null || !await userManager.CheckPasswordAsync(user, userAuthentication.Password))
            {
                return Ok(new LoginResponse { ErrorMessage = "Invalid" });
            }
            return Ok(this._authService.loginResponseAsync(userAuthentication));
        }
    }
}
