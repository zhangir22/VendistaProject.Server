using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Application.Services;
using VendistaProject.Application.Services.Interfaces;

namespace VendistaProject.Application.ServiecRegistration
{
    public static class ServiceRegistration
    {
        public static void RegistrationBllServices(this IServiceCollection services)
        { 
            services.AddScoped<IHistoryService, HistoryService>();
        }
    }
}
