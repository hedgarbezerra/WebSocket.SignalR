using FluentResults;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebSocket.SignalR.Configuration
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message} and was caught by global handler.", exception.Message);

            var error = new Error($"Something happened while processing your [{httpContext.Request.Method}] request to '{httpContext.Request.Path}'.")
                .CausedBy(exception.Message, exception)
                .WithMetadata("Occurred at", DateTime.UtcNow)
                .WithMetadata("Method", httpContext.Request.Method);

            var result = Result.Fail(error);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response
                .WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}
