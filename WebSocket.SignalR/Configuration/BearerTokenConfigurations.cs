using Microsoft.AspNetCore.Authentication.BearerToken;

namespace WebSocket.SignalR.Configuration
{
    public static class BearerTokenConfigurations
    {
        public static void Configure(BearerTokenOptions options, IConfiguration configuration)
        {
            var issuer = configuration.GetValue<string>("Bearer:Issuer");
            if(!string.IsNullOrWhiteSpace(issuer))
                options.ClaimsIssuer = issuer;

            options.BearerTokenExpiration = TimeSpan.FromMinutes(configuration.GetValue<double>("Bearer:Expiration", 15));
        }
    }
}
