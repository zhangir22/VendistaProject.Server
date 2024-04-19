using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Infrastructure.Repositories;
using VendistaProject.Infrastructure.Repositories.Interfaces;

namespace VendistaProject.Application.Services
{
    public abstract class HistoryService:IAbstractRepository
    {
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
        protected readonly IAbstractRepository repository;
        public HistoryService(ILogger logger, IMapper mapper, IAbstractRepository repository)
        {
            Logger = logger;
            Mapper = mapper;
            this.repository = repository;
        }

        public virtual async Task<IEnumerable<IHistoryModel>> GetAllAsync()
        {
            var models = await repository.GetAllAsync();
            if(models == null)
            {
                Logger.LogError($"Models is null");
            }
            return Mapper.Map<IEnumerable<IHistoryModel>>(models);
        }
        public async Task<IHistoryModel> CreateAsync(IHistoryModel model)
        {
            return Mapper.Map<IHistoryModel>(await repository.CreateAsync(model));
        }

        public async Task<IHistoryModel> DeleteAsync(IHistoryModel model)
        {
            return Mapper.Map<IHistoryModel>(await repository.DeleteAsync(model));
        }

        public async Task<IHistoryModel?> FindAsyncById(int id)
        {
            return Mapper.Map<IHistoryModel>(await repository.FindAsyncById(id));
        }
    }
}
