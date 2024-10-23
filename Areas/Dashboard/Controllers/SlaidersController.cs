using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Re.DAL.Data;
using Re.DAL.Models;
using Re.PL.Areas.Dashboard.ViewModels;
using Re.PL.Helpers;

namespace Re.PL.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SlaidersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
       
        public SlaidersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var blog = context.Sliders.ToList();
            var B = mapper.Map<IEnumerable<SliderVM>>(blog);
            return View(B);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SliderVMForm blo)
        {
            if (!ModelState.IsValid)
            {
                return View(blo);

            }
            blo.NameOfImg = FilesSet.UploadFile(blo.Image, "images");
            var blog = mapper.Map<Slider>(blo);
            context.Add(blog);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var x = context.Sliders.Find(id);
            if (x is null)
            {
                return NotFound();
            }
            var BlogDet = mapper.Map<SliderVM>(x);
            return View(BlogDet);
        }
        public IActionResult Edit(int id)
        {
            var service = context.Sliders.Find(id);
            if (service is null)
            {
                return NotFound();
            }
            var serviceView = mapper.Map<SliderVMForm>(service);

            return View(serviceView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SliderVMForm v)
        {
            var service = context.Sliders.Find(v.Id);

            if (service is null)
            {
                return NotFound();
            }
            if (v.Image is null)
            {
                ModelState.Remove("Image");
            }
            else
            {
                v.NameOfImg = FilesSet.UploadFile(v.Image, "images");
            }

            if (!ModelState.IsValid)
            {
                return View(v);
            }
            mapper.Map(v, service);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteSlide(int id)
        {
            var blog = context.Sliders.Find(id);
            if (blog is null)
            {
                return RedirectToAction(nameof(Index));
            }

            FilesSet.DeleteFile(blog.NameOfImg, "images");
            context.Sliders.Remove(blog);
            context.SaveChanges();
            return Ok(new { message = "Sliders Deleted" });
        }









    }
}
