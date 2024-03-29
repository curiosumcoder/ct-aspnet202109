﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WA50.Models;
using WA50.ViewModels;

namespace WA50.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// MVVM = ModelViewViewModel
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(HomeIndexViewModel vm)
        {
            using (var db = new Northwind.Store.Data.NWContext())
            {
                vm.Products = db.Products.Where(p => p.ProductName.Contains(vm.Filter)).ToList();
            }

            return View(vm);
        }

        /// <summary>
        /// Binding
        /// - Form fields
        /// - The request body (JSON)*
        /// - Route data
        /// - Query string parameters (?filter=queso)
        /// - Uploaded files
        /// [FromQuery], ...
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        public IActionResult IndexOLD(string filter)
        {
            IEnumerable<Northwind.Store.Model.Product> data;

            using (var db = new Northwind.Store.Data.NWContext())
            {
                data = db.Products.Where(p => p.ProductName.Contains(filter)).ToList();
            }

            //ViewData["products"] = data;
            ViewBag.products = data;

            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
