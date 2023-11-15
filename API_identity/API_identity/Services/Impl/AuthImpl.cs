using API_identity.DataTransferObjects;
using API_identity.Entities.Models;
using API_identity.JwtFeatures;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API_identity.Services.Impl
{
    public class AuthImpl : AuthService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly JwtHandler jwtHandler;

        public AuthImpl(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.jwtHandler = jwtHandler;
        }

        public async Task<LoginResponse> loginResponseAsync(UserAuthentication userAuthentication)
        {
            var user = await userManager.FindByNameAsync(userAuthentication.Email);

            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = jwtHandler.GetClaim(user);
            var tokenOptions = jwtHandler.JwtSecurityToken(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new LoginResponse { IsAuthSuccessful = true, Token = token };
        }

        public async Task<RegistrationResponseDto> RegistrationResponseDto(UserRegistertrationDto userRegistertrationDto)
        {
            var user = mapper.Map<User>(userRegistertrationDto);

            var result = await userManager.CreateAsync(user, userRegistertrationDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return new RegistrationResponseDto { Errors = errors, IsSuccessfulRegistration = false };
            }
            

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }

    }
}
