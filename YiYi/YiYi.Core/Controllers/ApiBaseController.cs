using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiYi.Core.Controllers
{
    public class ApiBaseController<IService> : Controller
    {
        protected IService Service;

        public ApiBaseController() { }

        public ApiBaseController(IService service)
        {
            this.Service = service;
        }

    }
}
