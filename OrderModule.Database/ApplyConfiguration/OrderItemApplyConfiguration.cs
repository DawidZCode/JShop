using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderModule.Domain.AggregateModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Database.ApplyConfiguration
{
    class OrderItemApplyConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("orderProduct", "dbo");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
               .ForSqlServerUseSequenceHiLo("orderitemseq");

            builder.Property<int>("OrderId")
           .IsRequired();
        }
    }
}
