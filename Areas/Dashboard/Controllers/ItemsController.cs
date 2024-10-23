using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Re.DAL.Data;
using Re.DAL.Models;
using Re.PL.Areas.Dashboard.ViewModels;
using Re.PL.Helpers;

namespace Re.PL.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ItemsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var items = context.Items.ToList();
            var itemView = mapper.Map<IEnumerable<ItemVM>>(items);
            return View(itemView);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var portfolios = context.Portfolios.ToList();
            var vm = new ItemVMForm
            {
                Portfolios = new SelectList(portfolios, "Id", "Name")
            };
            return View(vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(ItemVMForm blo)
        {
            if (!ModelState.IsValid)
            {
                return View(blo);
            }

            blo.NameOfImg = FilesSet.UploadFile(blo.Image, "images");
            var port = mapper.Map<Items>(blo);
            context.Add(port);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var x = context.Items.Find(id);
            if (x is null)
            {
                return NotFound();
            }
            var BlogDet = mapper.Map<ItemDe>(x);
            return View(BlogDet);
        }
        public IActionResult Edit(int id)
        {
            var service = context.Items.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            var serviceView = mapper.Map<ItemVMForm>(service);

            // Fetch portfolios from the database and populate the select list
            serviceView.Portfolios = new SelectList(context.Portfolios, "Id", "Name");

            return View(serviceView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ItemVMForm v)
        {
            var service = context.Items.Find(v.Id);

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


        public IActionResult DeleteItem(int id)
        {
            var blog = context.Items.Find(id);
            if (blog is null)
            {
                return RedirectToAction(nameof(Index));
            }

            FilesSet.DeleteFile(blog.NameOfImg, "images");
            context.Items.Remove(blog);
            context.SaveChanges();
            return Ok(new { message = "Items deleted" });
        }







    }
}
