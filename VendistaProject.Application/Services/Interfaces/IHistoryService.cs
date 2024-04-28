using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Infrastructure.Models;

namespace VendistaProject.Application.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<IHistoryModel>> GetAllAsync();
        Task<HistoryDto> CreateAsync(HistoryDto model);
        Task<IHistoryModel> DeleteAsync(int id);
        Task<IHistoryModel?> FindAsyncById(int id);
    }
}
