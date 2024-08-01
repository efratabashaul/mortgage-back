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
    public class NotificationRepository: IRepository<Notification>
    {
        private readonly IContext _context;
        public NotificationRepository(IContext context)
        {
            _context = context;
        }

        public async Task<Notification> AddItemAsync(Notification item)
        {
            item.created_at = DateTime.Now;
            await _context.Notifications.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Notifications.Remove(await GetAsync(id));
            await _context.save();
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await this._context.Notifications.ToListAsync();
        }

        public async Task<Notification> GetAsync(int id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task Post(Notification item)
        {
            item.created_at = DateTime.Now;
            Console.WriteLine("in Post niti in repository");
            Console.WriteLine("entity in post=" + item);
            await _context.Notifications.AddAsync(item);
            await _context.save();
        }

        public async Task UpdateAsync(int id, Notification entity)
        {
            var notification=await GetAsync(id);
            notification.Message = entity.Message;
            notification.created_at = DateTime.Now;
            notification.UserId = entity.UserId;
            notification.IsRead = entity.IsRead;
            Console.WriteLine("entity in post="+entity);
            await _context.save();
        }

        public async Task<Notification> UpdateItemAsync(int id, Notification entity)
        {
            Console.WriteLine();
            var notification = await GetAsync(id);
            notification.Message = entity.Message;
            notification.created_at = DateTime.Now;
            notification.UserId = entity.UserId;
            notification.IsRead = entity.IsRead;
            Console.WriteLine("in update noti async repos entity="+entity.IsRead);
            await _context.save();
            return notification;
        }
    }
}
