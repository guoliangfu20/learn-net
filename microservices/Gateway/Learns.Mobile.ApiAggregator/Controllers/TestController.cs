﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Learns.Mobile.ApiAggregator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public IActionResult GetTest()
        {
            return Content($"Learns.Mobile.ApiAggregator");
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Jwt()
        {
            return Content(User.FindFirst("Name").Value);
        }
    }
}
