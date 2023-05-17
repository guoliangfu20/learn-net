using Learns.Ordering.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Ordering.Infrastructure.EntityConfigureations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.Id);

            // 单独设置属性
            builder.ToTable("order");
            builder.Property(p => p.UserId).HasMaxLength(20);
            builder.Property(p => p.UserName).HasMaxLength(30);

            // 导航属性
            builder.OwnsOne(o => o.Address, a =>
            {
                a.WithOwner();
                a.Property(p => p.City).HasMaxLength(20);
                a.Property(p => p.Street).HasMaxLength(50);
                a.Property(p => p.ZipCode).HasMaxLength(10);
            });
        }
    }
}
