using System.Reflection;
using FCxLabs.Application.Extensions;
using FCxLabs.Infrastructure.DbContext;
using FCxLabs.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Secrets
builder.Configuration.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);

//Adding App Dependencies
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Run();

try 
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<UserDbContext>();
        await dbContext.Database.MigrateAsync();
        // or dbContext.Database.EnsureCreatedAsync();
    }
    
    // Middleware pipeline configuration

    app.Run();
} 
catch (Exception e) 
{
    app.Logger.LogCritical(e, "An exception occurred during the service startup");
}
finally
{
    // Flush logs or else you lose very important exception logs.
    // if you use Serilog you can do it via
    // await Log.CloseAndFlushAsync();
}
