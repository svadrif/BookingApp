using Application.Authentication;
using Application.DTOs.AppUserDTO;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;
        private readonly ILoggerManager _logger;

        public AuthenticationService(IMapper mapper, IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings, ILoggerManager logger)
        {
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<AppUserAuthResponse> AuthenticateAsync(Guid Id)
        {
            try
            {
                AppUserAuthResponse response = new AppUserAuthResponse();

                var appUser = await _unitOfWork.AppUsers.GetByIdAsync(Id);

                if (appUser != null)
                {
                    var securityTokenDescriptor = GenerateJWToken(appUser);
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.CreateToken(securityTokenDescriptor);
                    response.JWToken = new JwtSecurityTokenHandler().WriteToken(token);
                    response.GetAppUserDTO = _mapper.Map<GetAppUserDTO>(appUser);

                }
                if (string.IsNullOrEmpty(response.JWToken)) return null;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct info in {nameof(AuthenticateAsync)} action {ex}");
                return null;
            }
        }

        private SecurityTokenDescriptor GenerateJWToken(AppUser appUser)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                            new Claim(ClaimTypes.GivenName, appUser.FirstName),
                            new Claim(ClaimTypes.Name, appUser.LastName),
                            new Claim(ClaimTypes.Role, appUser.Role.ToString())
                        }),
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return descriptor;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Something went wrong in {nameof(AuthenticateAsync)} action {ex}");
                return null;
            }
        }
    }
}
