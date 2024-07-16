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

        public Task<Notification> AddItemAsync(Notification item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task Post(Notification item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Notification entity)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> UpdateItemAsync(int id, Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}
