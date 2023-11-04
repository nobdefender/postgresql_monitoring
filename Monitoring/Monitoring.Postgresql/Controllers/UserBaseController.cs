using Microsoft.AspNetCore.Mvc;

namespace Monitoring.Postgresql.Controllers;

public class UserBaseController : BaseController
{
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly ILogger<ControllerBase> _logger;

    public UserBaseController(IHttpContextAccessor httpContextAccessor, ILogger<ControllerBase> logger) : base(logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected int GetUserId()
    {
        int.TryParse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault()?.Value, out int userId);
        return userId;
    }
}