using Application.Authentication;

namespace Application.Interfaces.IServices
{
    public interface IAuthenticationService
    {
        Task<AppUserAuthResponse> AuthenticateAsync(Guid Id);
    }
}
