using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Infrastructure.Repositories;
using VendistaProject.Infrastructure.Repositories.Interfaces;

namespace VendistaProject.Infrastructure.RepositoryRegistration
{
    public static class RepositoryRegistration
    {
        public static void RegistrationDalRepository(this IServiceCollection services)
        {

            services.AddTransient<IAbstractRepository, HistoryRepository>();
            services.AddScoped<IAbstractRepository, HistoryRepository>();
        }
    }
}
