using Microsoft.AspNetCore.Mvc;

namespace Monitoring.Postgresql.Controllers;

public class BaseController : ControllerBase
{
    protected readonly ILogger _logger;

    public BaseController(ILogger logger)
    {
        _logger = logger;
    }

    protected async Task<IActionResult> Execute(
        Func<Task<dynamic?>> func,
        [System.Runtime.CompilerServices.CallerMemberName]
        string memberName = "unknown",
        [System.Runtime.CompilerServices.CallerFilePath]
        string sourceFilePath = "unknown")
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                var errorMessage = string.Join("; ", errors);

                var message = $"Model is invalid: {errorMessage}{Environment.NewLine}";
                _logger.LogError(message);

                return BadRequest();
            }

            var res = await func();
            return Ok(res == null ? null : res);
        }
        catch (UnauthorizedAccessException uaEx)
        {
            LogException(memberName, sourceFilePath, uaEx);
            return Unauthorized();
        }
        catch (Exception ex)
        {
            LogException(memberName, sourceFilePath, ex);
            return Problem();
        }
    }

    protected async Task<IActionResult> Execute(
        Task task,
        [System.Runtime.CompilerServices.CallerMemberName]
        string memberName = "unknown",
        [System.Runtime.CompilerServices.CallerFilePath]
        string sourceFilePath = "unknown")
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                var errorMessage = string.Join("; ", errors);

                var message = $"Model is invalid: {errorMessage}{Environment.NewLine}";
                _logger.LogError(message);

                return BadRequest();
            }

            await task;
            return Ok();
        }
        catch (UnauthorizedAccessException uaEx)
        {
            LogException(memberName, sourceFilePath, uaEx);
            return Unauthorized();
        }
        catch (Exception ex)
        {
            LogException(memberName, sourceFilePath, ex);
            return Problem();
        }
    }

    private void LogException(string memberName, string sourceFilePath, Exception ex)
    {
        _logger.LogError(
            $"Failed \"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{memberName}\" Exception: {ex.ToString()}{Environment.NewLine}");
    }
}