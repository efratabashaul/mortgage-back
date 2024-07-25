using AutoMapper;
using Common.Entities;
using Repositories.Entities;
using Repositories.Interface;
using Service.Interfaces;


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

        async Task<NotificationDto> IService<NotificationDto>.AddAsync(NotificationDto entity)
        {
            return _mapper.Map<NotificationDto>(await _repository.AddItemAsync(_mapper.Map<Notification>(entity)));
        }

        async Task IService<NotificationDto>.DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        async Task<List<NotificationDto>> IService<NotificationDto>.GetAllAsync()
        {
            return _mapper.Map<List<NotificationDto>>(await _repository.GetAllAsync());
        }

        async Task<NotificationDto> IService<NotificationDto>.GetAsync(int id)
        {
            return _mapper.Map<NotificationDto>(await _repository.GetAsync(id));
        }

        async Task IService<NotificationDto>.Post(NotificationDto item)
        {
            await _repository.Post(_mapper.Map<Notification>(item));
        }

        async Task IService<NotificationDto>.UpdateAsync(int id, NotificationDto entity)
        {
            await _repository.UpdateAsync(id, _mapper.Map<Notification>(entity));
        }

        async Task<NotificationDto> IService<NotificationDto>.UpdateItemAsync(int id, NotificationDto entity)
        {
            var updatedUser = await _repository.UpdateItemAsync(id, _mapper.Map<Notification>(entity));
            return _mapper.Map<NotificationDto>(updatedUser);
        }
    }
}
