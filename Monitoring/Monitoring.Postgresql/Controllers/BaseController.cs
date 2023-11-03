using Microsoft.AspNetCore.Mvc;

namespace Monitoring.Postgresql.Controllers;

public class BaseController : ControllerBase
{
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly ILogger<ControllerBase> _logger;

    public BaseController(IHttpContextAccessor httpContextAccessor, ILogger<ControllerBase> logger)
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