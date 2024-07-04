using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public  interface IRepository<T>
    {
       public Task<T> GetAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id, T entity);
        public Task Post(T item);
        public Task<T> AddItemAsync(T item);
        public Task<T> UpdateItemAsync(int id,T entity);

    }
}
