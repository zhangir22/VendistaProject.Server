using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VendistaProject.Infrastructure;
using VendistaProject.Server.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
 .SetBasePath(System.IO.Directory.GetCurrentDirectory())
 .AddJsonFile($"appsettings.json", optional: false)
 .AddJsonFile($"appsettings.Environment.json", optional: true)
 .AddEnvironmentVariables()
 .Build(); 

builder.Services.AddRazorPages();
var services = builder.Services;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


IConfiguration configuration = builder.Configuration;
services.AddDbContext<VendistaProejctDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString(InitializationDataBase.ConnectionString)), ServiceLifetime.Transient);
app.Run();
