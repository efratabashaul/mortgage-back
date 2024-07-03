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
    public class DocumentTypesRepository : IRepository<DocumentTypes>
    {
        private readonly IContext _context;

        public DocumentTypesRepository(IContext context)
        {
            _context = context;
        }
        public async Task<DocumentTypes> AddItemAsync(DocumentTypes item)
        {
            await _context.DocumentTypes.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            _context.DocumentTypes.Remove(await GetAsync(id));
            await _context.save();
        }

        public async Task<List<DocumentTypes>> GetAllAsync()
        {
            return await _context.DocumentTypes.ToListAsync();
        }

        public async Task<DocumentTypes> GetAsync(int id)
        {
            return await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Post(DocumentTypes item)
        {
            await _context.DocumentTypes.AddAsync(item);
            await _context.save();
        }

        public async Task UpdateAsync(int id, DocumentTypes entity)
        {
            var documentTypes = await GetAsync(id);
            documentTypes.Transaction_Type=entity.Transaction_Type;
            documentTypes.Document_Name=entity.Document_Name;
            documentTypes.Required=entity.Required;
            await _context.save();

        }
    }
}
