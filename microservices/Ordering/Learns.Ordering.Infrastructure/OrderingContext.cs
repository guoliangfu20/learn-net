﻿using DotNetCore.CAP;
using Learns.Ordering.Domain.OrderAggregate;
using Learns.Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Learns.Ordering.Infrastructure.EntityConfigureations;

namespace Learns.Ordering.Infrastructure
{
    public class OrderingContext : EFContext
    {
        public OrderingContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
