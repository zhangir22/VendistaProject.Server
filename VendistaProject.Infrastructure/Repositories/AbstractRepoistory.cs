using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Infrastructure.Repositories.Interfaces;

namespace VendistaProject.Infrastructure.Repositories
{
    public class AbstractRepoistory  : IAbstractRepository
    {
        protected readonly VendistaProejctDbContext _context;
        public AbstractRepoistory(VendistaProejctDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<IHistoryModel>> GetAllAsync()
        {
            return await _context.Histories.ToListAsync();
        }
        public async Task<IHistoryModel> CreateAsync(IHistoryModel model)
        {
            if(model != null)
            {
                if(await _context.Histories.FindAsync(model) == null)
                    await _context.AddAsync(model);
            }
            _context.SaveChanges();
            return _context.Histories.LastOrDefault();
        }

        public async Task<IHistoryModel> DeleteAsync(IHistoryModel model)
        {
            if(model != null)
            {
                if(await _context.Histories.FindAsync(model) != null)
                {
                    _context.Remove(model);
                }
            }
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IHistoryModel?> FindAsyncById(int id)
        {
            return await _context.Histories.FirstOrDefaultAsync(h => h.id == id);
        }
    }
}
