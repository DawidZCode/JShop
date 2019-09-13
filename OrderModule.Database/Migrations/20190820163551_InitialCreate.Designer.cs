﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderModule.Database;

namespace OrderModule.Database.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20190820163551_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.orderitemseq", "'orderitemseq', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:dbo.paymentseq", "'paymentseq', 'dbo', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderModule.Domain.AggregateModel.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "orderitemseq")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("OrderCount");

                    b.Property<int>("OrderId");

                    b.Property<int?>("OrdersId");

                    b.Property<string>("ProductCode");

                    b.Property<int>("ProductId");

                    b.Property<string>("ProductName");

                    b.Property<double>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrdersId");

                    b.ToTable("orderProduct","dbo");
                });

            modelBuilder.Entity("OrderModule.Domain.AggregateModel.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("OrderStatus");

                    b.Property<int?>("PaymentMethodId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("orders","dbo");
                });

            modelBuilder.Entity("OrderModule.Domain.AggregateModel.PaymentDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "paymentseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "dbo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("CreditCartNumber");

                    b.Property<string>("Cvv");

                    b.Property<DateTime>("ExpiredCardDate");

                    b.Property<string>("OwnerName");

                    b.HasKey("Id");

                    b.ToTable("payments","dbo");
                });

            modelBuilder.Entity("OrderModule.Domain.AggregateModel.OrderProduct", b =>
                {
                    b.HasOne("OrderModule.Domain.AggregateModel.Orders")
                        .WithMany("Products")
                        .HasForeignKey("OrdersId");
                });

            modelBuilder.Entity("OrderModule.Domain.AggregateModel.Orders", b =>
                {
                    b.HasOne("OrderModule.Domain.AggregateModel.PaymentDetails", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId");
                });
#pragma warning restore 612, 618
        }
    }
}
