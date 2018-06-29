using System;
using Microsoft.EntityFrameworkCore;
using VideoPay.Entities;

namespace VideoPay.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasIndex(o => o.OrderNo).IsUnique();
            modelBuilder.Entity<Order>().Property(o => o.CreateTime).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Order>().Property(o => o.LastUpdateTime).HasDefaultValue("GETDATE()");
        }
    }
}