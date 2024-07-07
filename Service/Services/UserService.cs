using AutoMapper;
using Common.Entities;
using Repositories.Entities;
using Repositories.Interface;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService :ILoginService //IService<UsersDto>
    {
        private readonly IMapper _mapper;
        private readonly ILogin _repository;

        public UserService(ILogin repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<UsersDto> AddAsync(UsersDto entity)
        {
            return _mapper.Map<UsersDto>(await _repository.AddItemAsync(_mapper.Map<Users>(entity)));
        }

        //public async Task<UsersDto> UpdateItemAsync(int id, UsersDto entity)
        //{
        //    return _mapper.Map<UsersDto>(await _repository.UpdateItemAsync(_mapper.Map<Users>(id, entity)));
        //}

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<UsersDto>> GetAllAsync()
        {
            return _mapper.Map<List<UsersDto>>(await _repository.GetAllAsync());
        }

        public async Task<UsersDto> GetAsync(int id)
        {
            return _mapper.Map<UsersDto>(await _repository.GetAsync(id));
        }

        public async Task Post(UsersDto item)
        {
            await _repository.Post(_mapper.Map<Users>(item));
        }

        public async Task UpdateAsync(int id, UsersDto entity)
        {
            await _repository.UpdateAsync(id, _mapper.Map<Users>(entity));
        }

        public async Task<UsersDto> UpdateItemAsync(int id, UsersDto entity)
        {
            var updatedUser = await _repository.UpdateItemAsync(id, _mapper.Map<Users>(entity));
            return _mapper.Map<UsersDto>(updatedUser);
        }

        public UsersDto Login(string email, string password)
        {
            return _mapper.Map<UsersDto>(_repository.getUserByLogin(email, password));
        }


    }
}
