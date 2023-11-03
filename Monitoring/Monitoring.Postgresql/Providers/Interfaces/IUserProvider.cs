using System.Security.Claims;
using Monitoring.Postgresql.Models.Auth;

namespace Monitoring.Postgresql.Providers.Interfaces;

public interface IUserProvider
{
    Task<IResult> LoginAsync(UserLoginRequest userLoginRequest, CancellationToken cancellationToken = default);
    Task<IResult> RefreshAsync(TokenApiModel tokens, CancellationToken cancellationToken = default);
    Task<UserDTO?> GetById(int? id, CancellationToken cancellationToken = default);
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    int? GetUserIdByJwtToken(string token);
    Task<IEnumerable<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<IResult> UpsertUserAsync(UpsertUserDTO model, CancellationToken cancellationToken);
    Task<IResult> SetPasswordAsync(SetPasswordDTO model, CancellationToken cancellationToken);
}