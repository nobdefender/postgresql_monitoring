using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Monitoring.Postgresql.Settings;
using Swashbuckle.AspNetCore.Annotations;

namespace Monitoring.Postgresql.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
public class AuthControllers : ControllerBase
{
    private readonly JwtSettings _jwtSettings;

    public AuthControllers(ILogger<AuthControllers> logger, JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    [SwaggerResponse(200, Type = typeof(string))]
    [Route("api/Auth/Generate")]
    [HttpGet]
    public string GenerateJwtToken(string name)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", name) }),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}