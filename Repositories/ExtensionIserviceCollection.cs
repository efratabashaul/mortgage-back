using Microsoft.Extensions.DependencyInjection;
using Repositories.Entities;
using Repositories.Interface;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public static class ExtensionIserviceCollection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped<IRepository<Users>,UsersRepository>();
            services.AddScoped<IRepository<AuditLogs>, AudioLogsRepository>();
            services.AddScoped<IRepository<Customers>, CustomerRepository>();
            services.AddScoped<IRepository<CustomerTasks>, CustomerTaskRepository>();
            services.AddScoped<IRepository<Notifications>, NotificationsRepository>();
            return services;
        }
     
    }
}
