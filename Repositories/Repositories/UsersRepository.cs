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
    public class UsersRepository : ILogin
    {
        private readonly IContext _context;
        public UsersRepository(IContext context)
        {
            _context = context;
        }

        public async Task<Users> AddItemAsync(Users item)
        {
            item.Created_at = DateTime.Now;
            item.Updated_at = DateTime.Now;
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
            item.Created_at= DateTime.Now;
            item.Updated_at= DateTime.Now;
            await _context.Users.AddAsync(item);
            await _context.save();
        }
        public async Task UpdateAsync(int id, Users entity)
        {
            var user = await GetAsync(id);
            user.UserName = entity.UserName != null ? entity.UserName : user.UserName;
            user.Password = entity.Password != null ? entity.Password : user.Password;
            user.Email = entity.Email != null ? entity.Email : user.Email;
            user.Role = entity.Role !=Role.None ? entity.Role : user.Role;
            user.Created_at = entity.Created_at != null ? entity.Created_at : user.Created_at;
            user.Updated_at = DateTime.Now;
            await _context.save();
        }
        public async Task<Users> UpdateItemAsync(int id, Users entity)
        {
            Console.WriteLine("in UpdateItemAsync");
            var user = await GetAsync(id);
            user.UserName = entity.UserName != "string" ? entity.UserName : user.UserName;
            user.Password = entity.Password != null ? entity.Password : user.Password;
            user.Email = entity.Email != "string" ? entity.Email : user.Email;
            user.Role = entity.Role != Role.None ? entity.Role : user.Role;
            user.Created_at = entity.Created_at != null ? entity.Created_at : user.Created_at;
            user.Updated_at = DateTime.Now;
            await _context.save();// Use Task.Run to wrap the synchronous save met hod
            return user;
        }

        public Users getUserByLogin(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
                return user;
            return null;
        }
        public Users getUserEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
                return user;
            return null;
        }

    }
}

