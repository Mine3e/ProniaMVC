using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProniaMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _categoryService.AddCategory(category);
            }
            catch(DuplicateCategoryException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            var category = _categoryService.GetAllCategories().FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category newCategory)
        {
            var oldCategory = _categoryService.GetAllCategories().FirstOrDefault(x => x.Id == newCategory.Id);
            if (oldCategory == null)
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            _categoryService.UpdateCategory(newCategory.Id, newCategory);

            return RedirectToAction(nameof(Index));
        }
    }
}
