using AutoMapper;
using Common.Entities;
using Repositories.Entities;
using Repositories.Interface;
using Repositories.Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CustomerService : IService<CustomersDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Customers> _repository;

        public CustomerService(IRepository<Customers> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CustomersDto> AddAsync(   CustomersDto entity)
        {
            return _mapper.Map<CustomersDto>(await _repository.AddItemAsync(_mapper.Map<Customers>(entity)));
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<CustomersDto>> GetAllAsync()
        {
            return _mapper.Map<List<CustomersDto>>(await _repository.GetAllAsync());
        }

        public async Task<CustomersDto> GetAsync(int id)
        {
            return _mapper.Map<CustomersDto>(await _repository.GetAsync(id));
        }

        public async Task Post(CustomersDto item)
        {
            await _repository.Post(_mapper.Map<Customers>(item));
        }

        public async Task UpdateAsync(int id, CustomersDto entity)
        {
            await _repository.UpdateAsync(id, _mapper.Map<Customers>(entity));
        }
    }
}
