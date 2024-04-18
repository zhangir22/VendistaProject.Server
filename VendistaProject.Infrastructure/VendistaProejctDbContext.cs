using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models;

namespace VendistaProject.Infrastructure
{
    public class VendistaProejctDbContext:DbContext
    {
        public VendistaProejctDbContext(DbContextOptions<VendistaProejctDbContext> options) 
            :base(options) { }
        public DbSet<History> Histories { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  
        }
    }
}
