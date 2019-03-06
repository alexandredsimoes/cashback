using Cashback.Infrastructure.Data.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models
{
    public class CashbackContext : DbContext
    {
        public CashbackContext()
        { }

        public CashbackContext(DbContextOptions<CashbackContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    //.UseInMemoryDatabase("cashback");
            //    .UseSqlite("Filename=Cashback.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenreEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GenreCashbackEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceInfoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BasketEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());

        }

        public DbSet<Album> Albuns { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreCashback> GenresCashback { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ServiceInfo> Services { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Basket> Basket { get; set; }
    }
}
