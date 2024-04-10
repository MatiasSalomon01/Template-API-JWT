using Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions;

public static class ResponseExtension
{
    public static IActionResult Action(this Response response)
    {
        ObjectResult? result = null;

        if (response.IsOk) result = new OkObjectResult(response.Content);
        if (response.IsNotFound) result = new NotFoundObjectResult(response.Content);
        if (response.IsBadRequest) result = new BadRequestObjectResult(response.Content);

        return result ?? new OkObjectResult(null);
    }
}
