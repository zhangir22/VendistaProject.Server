using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Infrastructure.Repositories.Interfaces;

namespace VendistaProject.Application.Services
{
    public abstract class AbstractService:IAbstractRepository
    {
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
        protected readonly IAbstractRepository repository;
        public AbstractService(ILogger logger, IMapper mapper, IAbstractRepository repository)
        {
            Logger = logger;
            Mapper = mapper;
            this.repository = repository;
        }

        public virtual async Task<IEnumerable<IHistoryModel>> GetAllAsync()
        {
            var models = await repository.GetAllAsync();

            return Mapper.Map<IEnumerable<IHistoryModel>>(models);
        }
        public Task<IHistoryModel> CreateAsync(IHistoryModel model)
        {
            return repository.CreateAsync(model);
        }

        public Task<IHistoryModel> DeleteAsync(IHistoryModel model)
        {
            return repository.DeleteAsync(model);
        }

        public Task<IHistoryModel?> FindAsyncById(int id)
        {
            return repository.FindAsyncById(id);
        }
    }
}
