using System.Security.Claims;
using Monitoring.Postgresql.Models.Auth;

namespace Monitoring.Postgresql.Providers.Interfaces;

public interface IWebUserProvider
{
    Task<IResult> WebUserLoginAsync(UserLoginRequest userLoginRequest, CancellationToken cancellationToken);
    Task<IResult> WebUserRefreshAsync(TokenApiModel tokens, CancellationToken cancellationToken);
    Task<WebUserDTO?> GetWebUserById(int? id, CancellationToken cancellationToken);
    int? GetWebUserIdByJwtToken(string token);
    Task<IEnumerable<WebUserDTO>> GetAllWebUsersAsync(CancellationToken cancellationToken);
    Task<IResult> UpsertWebUserAsync(UpsertWebUserDTO model, CancellationToken cancellationToken);
    Task<IResult> SetWebUserPasswordAsync(SetPasswordDTO model, CancellationToken cancellationToken);
}