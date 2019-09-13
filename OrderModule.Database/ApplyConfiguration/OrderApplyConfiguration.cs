using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderModule.Domain.AggregateModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Database.ApplyConfiguration
{
    public class OrderApplyConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.ToTable("orders", "dbo");
            builder.HasKey(o => o.Id);
            builder.Property<int>("UserId").IsRequired();
            builder.Property<DateTime>("OrderDate").IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Orders.Products));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
