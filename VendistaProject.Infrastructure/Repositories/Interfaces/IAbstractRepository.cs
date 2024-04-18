using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models.Interfaces;

namespace VendistaProject.Infrastructure.Repositories.Interfaces
{
    public interface IAbstractRepository<T> where T : IBaseModel, IHistoryModel
    {
        Task<T> CreateAsync(T model);
        Task<T> DeleteAsync(T model);
        Task<T?> FindAsyncById(int id);
    }
}
