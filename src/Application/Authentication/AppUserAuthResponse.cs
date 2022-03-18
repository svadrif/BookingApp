using Application.DTOs.AppUserDTO;

namespace Application.Authentication
{
    public class AppUserAuthResponse
    {
        public GetAppUserDTO GetAppUserDTO { get; set; }
        public string JWToken { get; set; } = string.Empty;
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
