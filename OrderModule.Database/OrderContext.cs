using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OrderModule.Database.ApplyConfiguration;
using OrderModule.Domain.AggregateModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrderModule.Database
{
    public class OrderContext : DbContext
    {

        public OrderContext(DbContextOptions<OrderContext> options): base(options)
        {

        }

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderProduct> OrderItems { get; set; }

        public DbSet<PaymentDetails> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentApplyConfiguration());
            modelBuilder.ApplyConfiguration(new OrderApplyConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemApplyConfiguration());
        }
    }
}
