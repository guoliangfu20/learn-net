using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceCodeWeb.Models
{
    public class PageViewModel<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
    }
}