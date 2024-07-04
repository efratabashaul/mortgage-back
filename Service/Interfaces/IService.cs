using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IService<T>
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetAsync(int id);
        public Task UpdateAsync(int id, T entity);
        public Task DeleteAsync(int id);
        public Task Post(T item);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateItemAsync(int id,T entity);
    }
}
