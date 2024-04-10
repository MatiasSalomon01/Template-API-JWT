using Core.Helpers;
using Core.Interfaces.Services;
using Core.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService auth;
    public AuthController(IAuthService auth)
    {
        this.auth = auth;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await auth.Login(request);
        return response.Action();
    }
}
