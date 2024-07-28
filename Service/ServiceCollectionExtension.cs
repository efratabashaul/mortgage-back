using Common.Entities;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Entities;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ServiceCollectionExtension 
    {
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        { 
            services.AddRepositories();
            services.AddScoped<ILoginService, UserService>();
            services.AddScoped<IService<CustomersDto>,CustomerService>();
            services.AddScoped<IService<CustomerTasksDto>,CustomerTaskService>();
            services.AddScoped<IService<LeadsDto>, LeadsService>();
            services.AddScoped<IService<DocumentTypesDto>, DocumentTypesService>();
            services.AddScoped<IService<NotificationDto>, NotificationService>();

            services.AddAutoMapper(typeof(MappeProfile));
            return services;
        }

    }
}
