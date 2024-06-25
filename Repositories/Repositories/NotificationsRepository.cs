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
    public class NotificationsRepository : IRepository<Notifications>
    {
        private readonly IContext _context;

        public NotificationsRepository(IContext contecxt){
            _context = contecxt; 
        }   
        public async Task<Notifications> AddItemAsync(Notifications item)
        {
            await _context.Notifications.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Notifications.Remove(await GetAsync(id));
            await _context.save();
        }

        public async Task<List<Notifications>> GetAllAsync()
        {
           return await _context.Notifications.ToListAsync();
        }

        public async Task<Notifications> GetAsync(int id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Post(Notifications item)
        {
            await _context.Notifications.AddAsync(item);
            await _context.save();
        }

        public async Task UpdateAsync(int id, Notifications entity)
        {
            var notification = await GetAsync(id);
            notification.Customer_id = entity.Customer_id;
            notification.Message = entity.Message;
            notification.Is_read = entity.Is_read;
            notification.created_at = entity.created_at;
        }
    }
}
