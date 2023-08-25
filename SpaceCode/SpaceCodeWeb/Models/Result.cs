﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceCodeWeb.Models
{
    public class ResultDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}