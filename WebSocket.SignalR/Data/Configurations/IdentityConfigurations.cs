using Microsoft.AspNetCore.Identity;

namespace WebSocket.SignalR.Data.Configurations
{
    internal static class IdentityConfigurationOptions
    {
        public static void Configure(IdentityOptions options)
        {
            ConfigureUser(options);
            ConfigurePassword(options);
            ConfigureSignIn(options);
        }

        private static void ConfigureUser(IdentityOptions options)
        {
            options.User.RequireUniqueEmail = true;
        }
        private static void ConfigurePassword(IdentityOptions options)
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
        }
        private static void ConfigureSignIn(IdentityOptions options)
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        }
    }
}
