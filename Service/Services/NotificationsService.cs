using AutoMapper;
using Common.Entities;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NotificationsService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<NotificationsDto> _repository;

        public NotificationsService(IRepository<NotificationsDto> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<NotificationsDto> AddAsync(NotificationsDto entity)
        {
            return _mapper.Map<NotificationsDto>(await _repository.AddItemAsync(_mapper.Map<NotificationsDto>(entity)));
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<NotificationsDto>> GetAllAsync()
        {
            return _mapper.Map<List<NotificationsDto>>(await _repository.GetAllAsync());
        }

        public async Task<NotificationsDto> GetAsync(int id)
        {
            return _mapper.Map<NotificationsDto>(await _repository.GetAsync(id));
        }

        public async Task Post(NotificationsDto item)
        {
            await _repository.Post(_mapper.Map<NotificationsDto>(item));
        }

        public async Task UpdateAsync(int id, UsersDto entity)
        {
            await _repository.UpdateAsync(id, _mapper.Map<NotificationsDto>(entity));
        }
    }
}
