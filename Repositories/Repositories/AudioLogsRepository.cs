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
    public class AudioLogsRepository : IRepository<AuditLogs>
    {
        private readonly IContext _context;
        public AudioLogsRepository(IContext context) { 
            _context = context;
        }
        public async Task<AuditLogs> AddItemAsync(AuditLogs item)
        {
            await _context.AuditLogs.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
           _context.AuditLogs.Remove(await GetAsync(id));
           await _context.save();
        }

        public async Task<List<AuditLogs>> GetAllAsync()
        {
            return await _context.AuditLogs.ToListAsync();
        }

        public async Task<AuditLogs> GetAsync(int id)
        {
            return await _context.AuditLogs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Post(AuditLogs item)
        {
            await _context.AuditLogs.AddAsync(item);
            await _context.save();
        }

        public async Task UpdateAsync(int id, AuditLogs entity)
        {
            var auditLog = await GetAsync(id);
            auditLog.User_id = entity.User_id;
            auditLog.Action = entity.Action;
            auditLog.Details = entity.Details;
            auditLog.Created_at = entity.Created_at;

        }
    }
}
