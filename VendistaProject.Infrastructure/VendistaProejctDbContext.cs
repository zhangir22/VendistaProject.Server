using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
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
        public VendistaProejctDbContext() { }
        public VendistaProejctDbContext(DbContextOptions<VendistaProejctDbContext> options) 
            :base(options) { }
        public DbSet<HistoryDto> Histories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_vendista;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
