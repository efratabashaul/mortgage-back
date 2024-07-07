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
    internal class DocumentTypesService : IService<DocumentTypesDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DocumentTypes> _repository;

        public DocumentTypesService(IRepository<DocumentTypes> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<DocumentTypesDto> AddAsync(DocumentTypesDto entity)
        {
            return _mapper.Map<DocumentTypesDto>(await _repository.AddItemAsync(_mapper.Map<DocumentTypes>(entity)));
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<DocumentTypesDto>> GetAllAsync()
        {
            return _mapper.Map<List<DocumentTypesDto>>(await _repository.GetAllAsync());
        }

        public async Task<DocumentTypesDto> GetAsync(int id)
        {
            return _mapper.Map<DocumentTypesDto>(await _repository.GetAsync(id));
        }

        public async Task Post(DocumentTypesDto item)
        {
            await _repository.Post(_mapper.Map<DocumentTypes>(item));
        }

        public async Task UpdateAsync(int id, DocumentTypesDto entity)
        {
            await _repository.UpdateAsync(id, _mapper.Map<DocumentTypes>(entity));
        }
    }
}
