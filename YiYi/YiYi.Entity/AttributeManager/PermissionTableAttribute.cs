using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiYi.Entity.AttributeManager
{
    public class PermissionTableAttribute : Attribute
    {
        /// <summary>
        /// 控制权
        /// </summary>
        public string Name { get; set; }
    }
}
