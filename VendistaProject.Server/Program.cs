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
using Microsoft.Extensions.Logging.Log4Net;
using VendistaProject.Infrastructure.Repositories;
using VendistaProject.Application.Services;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using VendistaProject.Infrastructure.Repositories.Interfaces;
using VendistaProject.Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration
 .SetBasePath(System.IO.Directory.GetCurrentDirectory()) 
 .AddJsonFile($"appsettings.Development.json", optional: true)
 .AddEnvironmentVariables()
 .Build(); 

builder.Services.AddRazorPages();


var services = builder.Services;
builder.Services.AddAutoMapper(typeof(Program));
services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});
services.AddEndpointsApiExplorer(); 
services.AddMvcCore();
services.AddMvc();
services.AddRazorPages();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Vendista API",
        Description = "Project for Vendista", 
        Contact = new OpenApiContact
        {
            Name = "Zhangir Yemishov",
            Email = "lleenovich@gmail.com", 
        },
         
    });
});

services.AddHttpContextAccessor();
services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});
 
services.AddEndpointsApiExplorer();

services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddLog4Net("log4net.config");
    logging.AddConsole();
}); 

services.AddMemoryCache(); 

IConfiguration configuration = builder.Configuration;
string connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<VendistaProejctDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString(connectionString)));



services.RegistrationDalRepository();
services.AddScoped<IAbstractRepository, HistoryRepository>();
services.RegistrationBllServices();
services.AddScoped<IHistoryService, HistoryService>();
services.RegistrationBllAutoMapper();
services.AddAutoMapper(typeof(MappingProfile));

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

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages(); 
app.MapControllers();



app.Run();
