using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class CustomerRepository : IRepository<Customers>
    {
        private readonly IContext _context;

        public CustomerRepository(IContext context) {
            _context = context;
        }
        public async Task<Customers> AddItemAsync(Customers item)
        {
           await _context.Customers.AddAsync(item);
           await _context.save();
           return item;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Customers.Remove(await GetAsync(id));
            await _context.save();
        }

        public async Task<List<Customers>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customers> GetAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Post(Customers item)
        {
            await _context.Customers.AddAsync(item);
            await _context.save();
        }

        public async Task UpdateAsync(int id, Customers entity)
        {
            var customer =await GetAsync(id);
            customer.First_Name = entity.First_Name;
            customer.Last_Name = entity.Last_Name;
            customer.Email = entity.Email;
            customer.Phone = entity.Phone;
            customer.Address = entity.Address;
            customer.LastSynced = entity.LastSynced;
            customer.IsArchived = entity.IsArchived;
            customer.created_at = entity.created_at;
            customer.updated_at = entity.updated_at;

        }
    }
}
