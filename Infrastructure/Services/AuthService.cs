using Core.Helpers;
using Core.Interfaces.Services;
using Core.Models.Auth;
using Core.Models.ModelOptions;
using Infrastructure.Helpers;
using Infrastructure.Mock;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

internal class AuthService : IAuthService
{
    private readonly JwtOptions jwtOptions;

    public AuthService(IOptions<JwtOptions> options)
    {
        jwtOptions = options.Value;
    }

    public async Task<Response> Login(LoginRequest request)
    {
        var user = MockData.Users.FirstOrDefault(x => x.Username == request.Username &&
                                                      x.Password == request.Password);

        if (user is null) return Response.NotFound("Usuario no encontrado");

        var jwtToken = JwtHelper.GenerateToken(user, jwtOptions);

        return Response.Ok(jwtToken);
    }
}
