using API_identity.DataTransferObjects;
using API_identity.Entities.Models;
using API_identity.JwtFeatures;
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
        private readonly IMapper mapper;
        private readonly JwtHandler jwtHandler;

        public AccountController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
        {
            this.jwtHandler = jwtHandler;
            this.userManager = userManager;
            this.mapper = mapper;
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistertrationDto registertrationDto)
        {
            if (registertrationDto == null || !ModelState.IsValid) {
                return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = false });
            }
            var user  = mapper.Map<User>(registertrationDto);

            var result = await userManager.CreateAsync(user, registertrationDto.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Ok(new RegistrationResponseDto { Errors = errors, IsSuccessfulRegistration = false });
            }

            return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = true });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserAuthentication userAuthentication)
        {
            var user = await userManager.FindByNameAsync(userAuthentication.Email);

            if(user == null || !await userManager.CheckPasswordAsync(user, userAuthentication.Password))
            {
                return Unauthorized(new LoginResponse { ErrorMessage = "Invalid" });
            }

            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = jwtHandler.GetClaim(user);
            var tokenOptions = jwtHandler.JwtSecurityToken(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new LoginResponse { IsAuthSuccessful = true, Token = token });
        }

    }
}
