﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiddlewareException.Exceptions;

namespace MiddlewareException.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Index()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var ex = exceptionHandlerPathFeature?.Error;

            var knownException = ex as IKnownException;
            if (knownException == null)
            {
                var logger = HttpContext.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
                logger.LogError(ex, ex.Message);

                knownException = KnownException.Unknown;
            }
            else
            {
                knownException = KnownException.FromKnownException(knownException);
            }
            return View(knownException);
        }
    }
}
