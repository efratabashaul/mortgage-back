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
    public class CustomerTaskService:IService<CustomerTasksDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerTasks> _repository;

        public CustomerTaskService(IRepository<CustomerTasks> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CustomerTasksDto> AddAsync(CustomerTasksDto entity)
        {
            return _mapper.Map<CustomerTasksDto>(await _repository.AddItemAsync(_mapper.Map<CustomerTasks>(entity)));
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<CustomerTasksDto>> GetAllAsync()
        {
            return _mapper.Map<List<CustomerTasksDto>>(await _repository.GetAllAsync());
        }

        public async Task<CustomerTasksDto> GetAsync(int id)
        {
            return _mapper.Map<CustomerTasksDto>(await _repository.GetAsync(id));
        }

        public async Task Post(CustomerTasksDto item)
        {
            await _repository.Post(_mapper.Map<CustomerTasks>(item));
        }

        public async Task UpdateAsync(int id, CustomerTasksDto entity)
        {
            await _repository.UpdateAsync(id, _mapper.Map<CustomerTasks>(entity));
        }

        public async Task<CustomerTasksDto> UpdateItemAsync(int id, CustomerTasksDto entity)
        {
            var updatedCustomerTask = await _repository.UpdateItemAsync(id, _mapper.Map<CustomerTasks>(entity));
            return _mapper.Map<CustomerTasksDto>(updatedCustomerTask);
        }
    }
}
