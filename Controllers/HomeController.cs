using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re.DAL.Data;
using Re.DAL.Models;
using Re.PL.ViewModels;
using System.Diagnostics;

namespace Re.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context,  ILogger<HomeController> logger)
        {
            this.context = context;
      
            _logger = logger;
        }

        public IActionResult Index()
        {
            var sliders = context.Sliders.ToList();
            var portfolios = context.Portfolios.ToList();
            var items = context.Items.ToList();
            var services = context.Servicesp.ToList();
       

            ViewBag.Sliders = sliders;
            ViewBag.Portfolios = portfolios;
            ViewBag.Items = items;
            ViewBag.Servicesp = services;
            ViewBag.Blogs = context.Blogs.ToList();
            return View();
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
