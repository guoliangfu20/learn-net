using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCodeModel
{
    public class PageDTO
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 0;
        public int TotalCount { get; set; }
        public bool IsOrder { get; set; }
        public string OrderBy { get; set; }
    }
}
