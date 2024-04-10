using Core.Entities;
using Core.Models.ModelOptions;
using Infrastructure.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(User user, JwtOptions options)
    {
        var handler = new JwtSecurityTokenHandler();

        byte[] key = Encoding.UTF8.GetBytes(options.Key);
        var securityKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(CustomClaimType.Username, user.Username)
        };

        var rolesClaims = user.Roles.Select(x => new Claim(CustomClaimType.Role, x.Name));
        claims.AddRange(rolesClaims);

        var securityToken = new JwtSecurityToken(
            options.Issuer,
            options.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: signingCredentials);

        return handler.WriteToken(securityToken);
    }

    public static string ReadToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.ToString();
    }
}
