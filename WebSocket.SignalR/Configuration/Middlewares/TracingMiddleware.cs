using System.Diagnostics;

namespace WebSocket.SignalR.Configuration.Middlewares
{
    public class TracingMiddleware
    {
        private readonly RequestDelegate _next;

        public TracingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ActivitySource source = new("WebSocket.SignalR.Tracing");
            // Start a new Activity
            var activity = source.StartActivity();
            activity?.Start();

            try
            {
                activity.AddTag("API Endpoint: {endpoint}", context.Request.Path);
                // Proceed with the next middleware in the pipeline
                await _next(context);
            }
            finally
            {
                // Stop the Activity when the response is sent
                activity?.Stop();
            }
        }
    }

}
