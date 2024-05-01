using Microsoft.AspNetCore.Identity;
using System.Reflection;
using WebSocket.SignalR.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddVersioning(builder.Configuration);
builder.Services.AddIdentitySupport(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddInternalServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperSetup).Assembly);


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
