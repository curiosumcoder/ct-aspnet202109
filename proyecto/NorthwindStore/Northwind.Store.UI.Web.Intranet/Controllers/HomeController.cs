﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Store.UI.Web.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Web.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            _logger.Log(LogLevel.Information, $"Home.Index ... {DateTime.Now}");
            //_logger.LogTrace("Este es un trace");
            //_logger.LogError("Este es un error");

            // throw new ApplicationException("¡Esta es una excepción!");
            //return NotFound();
            //return Forbid();
            //return Unauthorized();

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var exceptionMessage = "";

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is ApplicationException)
            {
                var ex = exceptionHandlerPathFeature?.Error;

                exceptionMessage = $"Error en la aplicación: {ex.Message}";
                _logger.LogError(exceptionMessage);
            }
            else if (exceptionHandlerPathFeature?.Error is Microsoft.Data.SqlClient.SqlException)
            {
                var ex = exceptionHandlerPathFeature?.Error;
                exceptionMessage = $"Error en la SQL Server: {ex.Message}";
                _logger.LogError(exceptionMessage);
            }

            if (exceptionHandlerPathFeature?.Path == "/")
            {
                exceptionMessage += " desde la raíz.";
            }

            return View("Error", new ErrorViewModel { RequestId = requestId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorWithCode(string code)
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if (statusCodeReExecuteFeature != null)
            {
                _ =
                    statusCodeReExecuteFeature.OriginalPathBase
                    + statusCodeReExecuteFeature.OriginalPath
                    + statusCodeReExecuteFeature.OriginalQueryString;
            }

            ViewBag.statusCode = code;

            return View("ErrorStatus", new ErrorViewModel { RequestId = requestId });
        }
    }
}
