using Azure.Core;
using FluentResults;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebSocket.SignalR.Extensions;

namespace WebSocket.SignalR.Configuration.Middlewares
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
            string requestedUri = httpContext.Request.GetDisplayUrl();
            _logger.LogError(exception, "Exception occurred at {RequestedUrl}: {Message} and was caught by global handler.", requestedUri, exception.Message);

            var error = new Error($"Something happened while processing your [{httpContext.Request?.Method}] request to '{requestedUri}'.");
            var result = Result.Fail(error).FromResult();

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response
                .WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}
