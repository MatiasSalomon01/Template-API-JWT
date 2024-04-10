using Infrastructure.Constants;
using Infrastructure.Mock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.CustomAttributes;

namespace WebApi.Controllers;

public class UsersController : BaseController
{
    [HttpGet("get-users")]
    public IActionResult GetUsers()
    {
        return Ok(MockData.Users);
    }

    [Authorize(Roles = RoleType.Admin)]
    [HttpGet("only-admin")]
    public IActionResult OnlyAdmin()
    {
        return Ok();
    }

    [AuthorizeMultiple(RoleType.Customer, RoleType.Seller)]
    [HttpGet("only-customer-or-seller")]
    public IActionResult OnlyCustomerOrSeller()
    {
        return Ok();
    }
}
