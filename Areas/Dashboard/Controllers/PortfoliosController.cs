using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Re.DAL.Data;
using Re.DAL.Models;
using Re.PL.Areas.Dashboard.ViewModels;
using Re.PL.Helpers;

namespace Re.PL.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class PortfoliosController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PortfoliosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var po = context.Portfolios.ToList();
            var portfol = mapper.Map<IEnumerable<PortfolioVM>>(po);
            return View(portfol);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortfolioVMForm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);

            }
            var ser = mapper.Map<Portfolios>(vm);
            context.Add(ser);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var x = context.Portfolios.Find(id);
            if (x is null)
            {
                return NotFound();
            }
            var BlogDet = mapper.Map<PotfolioDetails>(x);
            return View(BlogDet);
        }
        public IActionResult Edit(int id)
        {
            var service = context.Portfolios.Find(id);
            if (service is null)
            {
                return NotFound();
            }
            var serviceView = mapper.Map<PortfolioVMForm>(service);

            return View(serviceView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PortfolioVMForm v)
        {

            if (!ModelState.IsValid)
            {
                return View(v);
            }
            var service = context.Portfolios.Find(v.Id);
            if (service is null)
            {
                return NotFound();
            }
           
            mapper.Map(v, service);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
     
       
        public IActionResult DeletePortfolio(int id)
        {
            var blog = context.Portfolios.Find(id);
            if (blog == null)
            {
                return NotFound(new { message = "service not found" });
            }

            context.Portfolios.Remove(blog);
            context.SaveChanges();

            return Ok(new { message = "portfolio deleted successfully" });
        }

    }
}
