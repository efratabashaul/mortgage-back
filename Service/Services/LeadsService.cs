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
    public class LeadsService : IService<LeadsDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Leads> _repository;
        public LeadsService(IRepository<Leads> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<LeadsDto> AddAsync(LeadsDto entity)
        {
            Console.WriteLine("in addAsync" + entity);
            return _mapper.Map<LeadsDto>(await _repository.AddItemAsync(_mapper.Map<Leads>(entity)));
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<LeadsDto>> GetAllAsync()
        {
            return _mapper.Map<List<LeadsDto>>(await _repository.GetAllAsync());
        }

        public async Task<LeadsDto> GetAsync(int id)
        {
            return _mapper.Map<LeadsDto>(await _repository.GetAsync(id));
        }

        public async Task Post(LeadsDto item)
        {
            await _repository.Post(_mapper.Map<Leads>(item));
        }

        public async Task UpdateAsync(int id, LeadsDto entity)
        {
            Console.WriteLine("from UpdateAsync  in LeadsService"+entity);
            await _repository.UpdateAsync(id, _mapper.Map<Leads>(entity));
        }
    }
}
