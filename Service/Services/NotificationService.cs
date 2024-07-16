using AutoMapper;
using Common.Entities;
using Repositories.Entities;
using Repositories.Interface;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NotificationService : IService<NotificationDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Notification> _repository;

        public NotificationService(IRepository<Notification> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        Task<NotificationDto> IService<NotificationDto>.AddAsync(NotificationDto entity)
        {
            throw new NotImplementedException();
        }

        Task IService<NotificationDto>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<NotificationDto>> IService<NotificationDto>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<NotificationDto> IService<NotificationDto>.GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IService<NotificationDto>.Post(NotificationDto item)
        {
            throw new NotImplementedException();
        }

        Task IService<NotificationDto>.UpdateAsync(int id, NotificationDto entity)
        {
            throw new NotImplementedException();
        }

        Task<NotificationDto> IService<NotificationDto>.UpdateItemAsync(int id, NotificationDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
