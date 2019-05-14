using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LSScraper.Models;
using LSScraper.Api;

namespace LSScraper.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var api = new Api();
            List<Article> articles = LSApi.ScrapeLS();

            return View(articles);
        }

    }
}
