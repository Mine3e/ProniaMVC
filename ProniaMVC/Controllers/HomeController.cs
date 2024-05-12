using Core.Models;
using Data.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVC.Areas.Admin.ViewModel;
using ProniaMVC.Models;
using System.Diagnostics;

namespace ProniaMVC.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Product> products = _context.Products.Include(x => x.ProductPhotos).Where(x => x.ProductPhotos.Count > 0).ToList();

            HomeVM homeVm = new HomeVM()
            {
                Products = products,
                Sliders = _context.Sliders.ToList()
            };

            return View(homeVm);
        }
        public IActionResult Detail(int? id)
        {
            return View();
        }
    }
}
