using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Infrastructure.Repositories.Interfaces;

namespace VendistaProject.Infrastructure.Repositories
{
    public class AbstractRepoistory<T> : IAbstractRepository<T> where T : IBaseModel, IHistoryModel
    {
        public Task<T> CreateAsync(T model)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T model)
        {
            throw new NotImplementedException();
        }

        public Task<T?> FindAsyncById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
