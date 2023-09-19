using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductAppAPI.Entities;
using ProductListApp.Domain.Entities;
using ProductWebAPI.Entities;

namespace ProductListApp.Persistence.Contexts
{
    public class ProductListAppDbContext : DbContext
    {
        public ProductListAppDbContext(DbContextOptions<ProductListAppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currency { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency {Name="Lira",Code="TRY",Symbol= '₺' }, 
                new Currency {Name="US Dollar",Code="USD", Symbol='$' },
                new Currency {Name="Euro",Code="EUR", Symbol= '€' }
                );
        }
    }
}
