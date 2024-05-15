using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebSocket.SignalR;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Models;
using WebSocket.SignalR.Configuration.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(ops =>
    {
        ops.JsonSerializerOptions.PropertyNamingPolicy = null;
        ops.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
        ops.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        ops.JsonSerializerOptions.WriteIndented = true;
        ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        ops.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        ops.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddVersioning(builder.Configuration);
builder.Services.AddIdentitySupport(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddInternalServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperSetup).Assembly);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (var description in app.DescribeApiVersions())
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
    }
});

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors(opt =>
{
    opt.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
});

app.MapGroup("api/identity")
    .MapIdentityApi<AppUser>();
app.MapControllers();

app.Run();
