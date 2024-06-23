using AutoMapper;
using Common.Entities;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    public class MappeProfile:Profile
    {
        public MappeProfile()
        {
            CreateMap<AuditLogsDto, AuditLogs>().ReverseMap();
            CreateMap<CustomersDto, Customers>().ReverseMap();
            CreateMap<CustomerTasksDto, CustomerTasks>().ReverseMap();
            CreateMap<NotificationsDto, Notifications>().ReverseMap();
            CreateMap<UsersDto, Users>().ReverseMap();
        }
    }
}
