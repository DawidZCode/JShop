using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderModule.Domain.AggregateModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Database.ApplyConfiguration
{
    class PaymentApplyConfiguration : IEntityTypeConfiguration<PaymentDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentDetails> builder)
        {
            builder.ToTable("payments", "dbo");
            builder.HasKey(o => o.Id);

            builder.Property(b => b.Id)
               .ForSqlServerUseSequenceHiLo("paymentseq", "dbo");

        }
    }
}