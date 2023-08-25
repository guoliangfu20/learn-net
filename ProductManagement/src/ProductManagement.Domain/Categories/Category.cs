using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProductManagement.Categories
{
    /// <summary>
    /// 
    /// 实现聚合根，AuditedAggregateRoot<Guid> 
    /// </summary>
    public class Category : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
    }
}
