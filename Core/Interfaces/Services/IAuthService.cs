using Core.Helpers;
using Core.Models.Auth;

namespace Core.Interfaces.Services;

public interface IAuthService
{
    Task<Response> Login(LoginRequest request);
}
