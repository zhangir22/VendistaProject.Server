
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Infrastructure.Repositories.Interfaces;

namespace VendistaProject.Infrastructure.Repositories
{
    public class HistoryRepository : AbstractRepoistory, IHistoryRepository
    {
        public HistoryRepository(VendistaProejctDbContext context) : base(context)
        {
        }
    }
}
