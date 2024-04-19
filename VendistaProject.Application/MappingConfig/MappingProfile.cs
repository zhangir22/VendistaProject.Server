using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models;
using VendistaProject.Infrastructure.Models;

namespace VendistaProject.Application.MappingConfig
{
    internal class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<HistoryDto, HistoryModel>();
        }
    }
}
