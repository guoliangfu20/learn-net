using Learns.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learns.Domain.OrderAggregate
{
    public class Order : Entity<long>, IAggregateRoot
    {
        public string UserId { get; }

        public string UserName { get; private set; }

        public Address Address { get; private set; }

        public int ItemCount { get; private set; }

        protected Order() { }

        public Order(string userId, string userName, int itemCount, Address address)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.ItemCount = itemCount;
            this.Address = address;
        }

        /// <summary>
        /// 领域模型的操作，定义为具有业务逻辑的方法.
        /// </summary>
        /// <param name="address"></param>
        public void ChangeAddress(Address address)
        {
            // 省去校验逻辑...
            this.Address = address;
        }
    }
}
