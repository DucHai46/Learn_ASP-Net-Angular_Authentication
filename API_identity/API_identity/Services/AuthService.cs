using API_identity.DataTransferObjects;

namespace API_identity.Services
{
    public interface AuthService
    {
        public Task<RegistrationResponseDto> RegistrationResponseDto( UserRegistertrationDto userRegistertrationDto);
        public Task<LoginResponse> loginResponseAsync(UserAuthentication userAuthentication);
    }
}
