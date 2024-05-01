using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebSocket.SignalR;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddVersioning(builder.Configuration);
builder.Services.AddIdentitySupport(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddInternalServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperSetup).Assembly);


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (var description in app.DescribeApiVersions())
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
    }
});
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapGroup("api/identity")
    .MapIdentityApi<AppUser>();

app.MapControllers();

app.Run();
