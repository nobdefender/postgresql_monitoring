using System.Security.Claims;
using Monitoring.Postgresql.Models.Auth;
using Monitoring.Postgresql.Providers.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Postgresql.Settings;
using AutoMapper;
using Monitoring.Posgresql.Infrastructure.Models.WebAuth;

namespace Monitoring.Postgresql.Providers.Implementations;

public class WebWebUserProvider : IWebUserProvider
{
    private const string PASSWORD_SALT = "passSalt";

    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;
    private readonly MonitoringServiceDbContext _monitoringServiceDbContext;

    public WebWebUserProvider(MonitoringServiceDbContext monitoringServiceDbContext, JwtSettings jwtSettings, IMapper mapper)
    {
        _mapper = mapper;
        _jwtSettings = jwtSettings;
        _monitoringServiceDbContext = monitoringServiceDbContext;
    }

    public Task<IEnumerable<WebUserDTO>> GetAllWebUsersAsync(CancellationToken cancellationToken)
    {
        var users = _monitoringServiceDbContext.WebUsers;
        return Task.FromResult(users.Select(_mapper.Map<WebUserDTO>));
    }

    public async Task<IResult> WebUserLoginAsync(UserLoginRequest userLoginRequest,
        CancellationToken cancellationToken = default)
    {
        var existedUser =
            await _monitoringServiceDbContext.WebUsers.FirstOrDefaultAsync(u => u.Username == userLoginRequest.Username,
                cancellationToken);
        if (existedUser == null)
        {
            return Results.NotFound("Пользователь не найден");
        }

        var passwordIsValid = GetPasswordHash(userLoginRequest.Password)!.Equals(existedUser.Password);

        if (!passwordIsValid)
        {
            return Results.BadRequest("Некорректный пароль");
        }

        var userDTO = _mapper.Map<WebUserDTO>(existedUser);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, $"{userDTO?.Id}"),
            new Claim(ClaimTypes.Email, userDTO.EmailAddress),
            new Claim(ClaimTypes.GivenName, userDTO.Name),
            new Claim(ClaimTypes.Surname, userDTO.LastName),
            new Claim(ClaimTypes.Role, userDTO.Role)
        };

        var accessToken = GenerateAccessToken(claims);
        var refreshToken = GenerateRefreshToken();

        existedUser.RefreshToken = refreshToken;
        existedUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDaysExpire);

        await _monitoringServiceDbContext.SaveChangesAsync(cancellationToken);

        return Results.Ok(new UserWithTokenDTO()
        {
            WebUser = userDTO,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    public async Task<IResult> WebUserRefreshAsync(TokenApiModel? tokens, CancellationToken cancellationToken)
    {
        if (tokens is null)
        {
            return Results.BadRequest("Необходим токен");
        }

        var accessToken = tokens.AccessToken;
        var refreshToken = tokens.RefreshToken;

        var principal = GetPrincipalFromExpiredToken(accessToken);
        var userId = Convert.ToInt32(principal.Identity.Name);

        var user = await _monitoringServiceDbContext.WebUsers.FirstOrDefaultAsync(w => w.Id == userId,
            cancellationToken: cancellationToken);
        if (user is null)
        {
            return Results.BadRequest("Пользователь не найден");
        }

        if (user.RefreshToken != refreshToken)
        {
            return Results.BadRequest("Несоответствие Refresh токена");
        }

        if (user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Results.BadRequest("Истекло время Rfresh токена");
        }

        var newAccessToken = GenerateAccessToken(principal.Claims);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _monitoringServiceDbContext.SaveChangesAsync(cancellationToken);

        return Results.Ok(new TokenApiModel
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    public async Task<WebUserDTO?> GetWebUserById(int? id, CancellationToken cancellationToken = default)
    {
        var user = await _monitoringServiceDbContext.WebUsers.FirstOrDefaultAsync(w => w.Id == id,
            cancellationToken: cancellationToken);
        return user != null ? _mapper.Map<WebUserDTO>(user) : null;
    }

    public async Task<IResult> UpsertWebUserAsync(UpsertWebUserDTO model, CancellationToken cancellationToken)
    {
        var dbModel = _mapper.Map<WebUserDbModel>(model);

        var existedUser = await _monitoringServiceDbContext.WebUsers.AsNoTracking()
            .FirstOrDefaultAsync(p => p.EmailAddress == dbModel.EmailAddress, cancellationToken);

        if (existedUser is null)
        {
            await _monitoringServiceDbContext.WebUsers.AddAsync(dbModel, cancellationToken);
            await _monitoringServiceDbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok();
        }

        dbModel.Id = existedUser.Id;
        dbModel.Password = existedUser.Password;
        existedUser = dbModel;
        _monitoringServiceDbContext.Entry(existedUser).State = EntityState.Modified;

        await _monitoringServiceDbContext.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }

    public async Task<IResult> SetWebUserPasswordAsync(SetPasswordDTO model, CancellationToken cancellationToken)
    {
        var user = await _monitoringServiceDbContext.WebUsers.AsNoTracking()
            .FirstOrDefaultAsync(p => p.EmailAddress == model.EmailAddress.ToLower(), cancellationToken);

        if (user is null)
        {
            return Results.NotFound("Пользователь не найден");
        }

        user.Password = GetPasswordHash(model.Password);
        _monitoringServiceDbContext.Entry(user).State = EntityState.Modified;

        await _monitoringServiceDbContext.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid Token");
        }

        return principal;
    }

    public int? GetWebUserIdByJwtToken(string token)
    {
        var secret = _jwtSettings.Key;
        var key = Encoding.ASCII.GetBytes(secret);
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        var claims = handler.ValidateToken(token, validations, out var tokenSecure);
        var stringId = claims.FindFirst(ClaimTypes.Name)?.Value;
        if (stringId == null) return null;
        var id = int.Parse(stringId);
        return id;
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var randomNumber = RandomNumberGenerator.Create();
        randomNumber.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenMinutesExpire),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string? GetPasswordHash(string password)
    {
        var salt = Encoding.ASCII.GetBytes(PASSWORD_SALT);
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100,
            numBytesRequested: 64));

        return hashed;
    }
}