﻿using Microsoft.AspNetCore.Mvc;

namespace Re.PL.Areas.Dashboard.Controllers
{
    [Area ("Dashboard")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
