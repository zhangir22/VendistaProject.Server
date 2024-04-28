using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Infrastructure.Repositories.Interfaces;

namespace VendistaProject.Infrastructure.Repositories
{
    public class AbstractRepoistory  : IAbstractRepository
    {
        protected VendistaProejctDbContext _context;
        public AbstractRepoistory(VendistaProejctDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<IHistoryModel>> GetAllAsync()
        {
            return await _context.Histories.ToListAsync();
        }
        public async Task<HistoryDto> CreateAsync(HistoryDto model)
        {
            _context.Histories.Add(model);
            await  _context.SaveChangesAsync();
            return model;
        }

        public async Task<IHistoryModel> DeleteAsync(int id)
        {
            var all = await GetAllAsync();
            var model =(HistoryDto)all.FirstOrDefault(c => c.id == id);
            _context.Histories.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IHistoryModel?> FindAsyncById(int id)
        {
            return await _context.Histories.FirstOrDefaultAsync(h => h.id == id);
        }
    }
}
