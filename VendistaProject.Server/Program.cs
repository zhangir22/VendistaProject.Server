using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VendistaProject.Application.MappingConfig;
using VendistaProject.Application.ServiecRegistration;
using VendistaProject.Infrastructure;
using VendistaProject.Infrastructure.RepositoryRegistration;
using VendistaProject.Server.Core;
using VendistaProject.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration
 .SetBasePath(System.IO.Directory.GetCurrentDirectory())
 .AddJsonFile($"appsettings.json", optional: false)
 .AddJsonFile($"appsettings.Environment.json", optional: true)
 .AddEnvironmentVariables()
 .Build(); 

builder.Services.AddRazorPages();


var services = builder.Services;
builder.Services.AddAutoMapper(typeof(Program));

services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});
services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});
services.AddEndpointsApiExplorer();
services.AddLogging(logging =>
{
    logging.ClearProviders(); 
    logging.AddConsole();
});

services.AddMemoryCache();
services.RegistrationDalRepository();
services.RegistrationBllServices();
services.RegistrationBllAutoMapper();

IConfiguration configuration = builder.Configuration;
services.AddDbContext<VendistaProejctDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString(InitializationDataBase.ConnectionString)), ServiceLifetime.Transient);




var app = builder.Build();

InitializationDataBase.Init(app);
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages(); 
app.MapControllers();



app.Run();
