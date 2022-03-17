using Application.DTOs.AppUserDTO;

namespace Application.Authentication
{
    public class AppUserAuthResponse
    {
        public GetAppUserDTO GetAppUserDTO { get; set; }
        public string JWToken { get; set; } = string.Empty;
    }
}
