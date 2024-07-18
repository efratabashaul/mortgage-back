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
            CreateMap<CustomersDto, Customers>().ReverseMap();
            CreateMap<CustomerTasksDto, CustomerTasks>().ReverseMap();
            CreateMap<UsersDto, Users>().ReverseMap();
            CreateMap<LeadsDto, Leads>().ReverseMap();
            CreateMap<DocumentTypesDto, DocumentTypes>().ReverseMap();
            CreateMap<NotificationDto,Notification>().ReverseMap();
        }
    }
}
