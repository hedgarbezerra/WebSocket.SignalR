using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Compact;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using WebSocket.SignalR.Data;
using WebSocket.SignalR.Data.Configurations;
using WebSocket.SignalR.Interfaces;
using WebSocket.SignalR.Models;
using WebSocket.SignalR.Services;

namespace WebSocket.SignalR.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            var context = services.BuildServiceProvider().GetRequiredService<IWebHostEnvironment>();
            services.AddSerilog(opt =>
            {
                opt.Enrich.FromLogContext();
                opt.WriteTo.File(Path.Combine(context.WebRootPath, "logs", "diagnostics-.txt"),
                    rollingInterval: RollingInterval.Minute,
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1));
                opt.WriteTo.File(new RenderedCompactJsonFormatter(), Path.Combine(context.WebRootPath, "jsonlogs", "diagnostics-.json"),
                    rollingInterval: RollingInterval.Minute,
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1));
            });

            return services;
        }
        public static IServiceCollection AddVersioning(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            double apiVersion = configuration.GetValue<double>(Constants.Api.ApiVersion);
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(apiVersion);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));
            })
            .EnableApiVersionBinding()
            .AddMvc()
            .AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
            });

            return services;
        }

        public static IServiceCollection AddIdentitySupport(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.EnableDetailedErrors();
                opt.UseSqlServer(connectionString, sqlOpt =>
                {
                    sqlOpt.EnableRetryOnFailure(3);
                });
            });

            services.AddIdentityCore<AppUser>(opt => IdentityConfigurationOptions.Configure(opt))
                .AddEntityFrameworkStores<AppDbContext>()
                .AddApiEndpoints();

            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme, opt => BearerTokenConfigurations.Configure(opt, configuration));
            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;
                var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());

                return new UriService(uri);
            });

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attrs = type.GetCustomAttributes<BindInterfaceAttribute>();
                    if (!attrs.Any())
                        continue;

                    foreach (var attr in attrs)
                    {
                        var serviceDescription = new ServiceDescriptor(attr.Interface, type, attr.Lifetime);
                        services.Add(serviceDescription);
                    }
                }
            }
            return services;
        }
    }
}
