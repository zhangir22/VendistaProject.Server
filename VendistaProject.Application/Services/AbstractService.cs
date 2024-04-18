using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendistaProject.Application.Services
{
    public abstract class AbstractService
    {
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;

    }
}
