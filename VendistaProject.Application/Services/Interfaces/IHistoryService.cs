using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models.Interfaces;

namespace VendistaProject.Application.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<IHistoryModel>> GetAllAsync();
        Task CreateAsync(IHistoryModel model);
        Task DeleteAsync(IHistoryModel model);
        Task<IHistoryModel?> FindAsyncById(int id);
    }
}
