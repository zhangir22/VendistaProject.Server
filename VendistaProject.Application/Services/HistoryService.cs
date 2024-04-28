using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Application.Services.Interfaces;
using VendistaProject.Dto.Models;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Infrastructure.Repositories;
using VendistaProject.Infrastructure.Repositories.Interfaces;

namespace VendistaProject.Application.Services
{
    public class HistoryService:IHistoryService
    {
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
        IAbstractRepository repository { get; set; }
        public HistoryService(ILogger<IAbstractRepository> logger, IMapper mapper, IAbstractRepository repository)
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
        public async Task<HistoryDto> CreateAsync(HistoryDto model)
        { 
            return await repository.CreateAsync(model);
        }

        public async Task<IHistoryModel> DeleteAsync(int id)
        {
            return Mapper.Map<IHistoryModel>(await repository.DeleteAsync(id));
        }

        public async Task<IHistoryModel?> FindAsyncById(int id)
        {
            return Mapper.Map<IHistoryModel>(await repository.FindAsyncById(id));
        }
        protected virtual Task BeforeCreate(IHistoryModel model, IHistoryModel dto) => Task.CompletedTask;
        protected virtual Task AfterCreate(IHistoryModel dto) => Task.CompletedTask;
    }
}
