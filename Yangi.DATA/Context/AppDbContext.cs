using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangi.DATA.Models;

namespace Yangi.DATA.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<Country> Countrieses { get; set; }
        public DbSet<Region> Regionses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(p => p.Regions)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
