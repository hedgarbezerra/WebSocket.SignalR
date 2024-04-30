using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebSocket.SignalR.Configuration;
using WebSocket.SignalR.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddVersioning(builder.Configuration);
builder.Services.AddIdentitySupport(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
        }
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapGroup("/identity").MapIdentityApi<IdentityUser>();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
