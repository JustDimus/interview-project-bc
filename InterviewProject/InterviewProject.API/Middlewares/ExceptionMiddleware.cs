using System.Net;
using InterviewProject.Core.Exceptions;

namespace InterviewProject.API.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ServiceException e)
        {
            _logger.LogError(e, $"Service exception handled: {e.ErrorType}");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unhandled exception");
            throw;
        }
    }
}