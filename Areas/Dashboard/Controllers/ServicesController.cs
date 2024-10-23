using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re.DAL.Data;
using Re.DAL.Models;
using Re.PL.Areas.Dashboard.ViewModels;
using Re.PL.Helpers;
using System.Security.Policy;
namespace Re.PL.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServicesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var s = context.Servicesp.ToList();
            var svm = mapper.Map<IEnumerable<ServiceVM>>(s);
            return View(svm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceVmForm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);

            }
            var ser = mapper.Map<Services>(vm);
            context.Add(ser);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var x = context.Servicesp.Find(id);
            if (x is null)
            {
                return NotFound();
            }
            var SerDe = mapper.Map<SerDelailsVM>(x);
            return View(SerDe);
        }


        public IActionResult Edit(int id)
        {
            var service = context.Servicesp.Find(id);
            if (service is null) {
                return NotFound();
            }
            var serviceView = mapper.Map<ServiceVmForm>(service);

            return View(serviceView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceVmForm v)
        {

            if (!ModelState.IsValid)
            {
                return View(v);
            }
            var service = context.Servicesp.Find(v.Id);
            if (service is null)
            {
                return NotFound();
            }
            // var serviceView = mapper.Map<ServiceVmForm>(service);
            mapper.Map(v, service);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteService(int id)
        {
            var blog = context.Servicesp.Find(id); 
            if (blog == null)
            {
                return NotFound(new { message = "service not found" });
            }

            context.Servicesp.Remove(blog);
            context.SaveChanges();

            return Ok(new { message = "service deleted successfully" });
        }

    }
}
