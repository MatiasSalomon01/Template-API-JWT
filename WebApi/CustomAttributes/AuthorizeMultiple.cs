using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.CustomAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeMultiple : Attribute, IAuthorizationFilter
{
    private string[] Roles { get; }

    public AuthorizeMultiple(string first, string second)
    {
        Roles = new[] { first, second };
    }

    public AuthorizeMultiple(string first, string second, string third)
    {
        Roles = new[] { first, second, third };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated) context.Result = new UnauthorizedObjectResult(null);

        if (!user.Claims.Any(x => Roles.Contains(x.Value))) context.Result = new ForbidResult();
    }
}
