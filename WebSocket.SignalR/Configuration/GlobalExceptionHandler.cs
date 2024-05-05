using Azure.Core;
using FluentResults;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Extensions;

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

            string requestedUri = string.Concat(httpContext.Request?.Scheme, "://", httpContext.Request?.Host.ToUriComponent());
            var error = new Error($"Something happened while processing your [{httpContext.Request?.Method}] request to '{requestedUri}'.");

            var result = Result.Fail(error).FromResult();

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response
                .WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}
