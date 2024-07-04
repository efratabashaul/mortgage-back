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
    public class UsersRepository : IRepository<Users>
    {
        private readonly IContext _context;
        public UsersRepository(IContext context)
        {
            _context = context;
        }

        public async Task<Users> AddItemAsync(Users item)
        {
            await _context.Users.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Users.Remove(await GetAsync(id));
            await _context.save();
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Post(Users item)
        {
            await _context.Users.AddAsync(item);
            await _context.save();
        }

        public async Task UpdateAsync(int id, Users entity)
        {
            var user=await GetAsync(id);
            user.UserName = entity.UserName;
            user.Password = entity.Password;
            user.Email = entity.Email;
            user.Role = entity.Role;
            user.Created_at = entity.Created_at;
            user.Updated_at= DateTime.UtcNow;
            await _context.save();
        }



        public async Task<Users> UpdateItemAsync(int id, Users entity)
        {
            var user = await GetAsync(id);
            user.UserName = entity.UserName;
            user.Password = entity.Password;
            user.Email = entity.Email;
            user.Role = entity.Role;
            user.Created_at = entity.Created_at;
            await _context.save();// Use Task.Run to wrap the synchronous save method
            return user;
        }

       
    }
}

