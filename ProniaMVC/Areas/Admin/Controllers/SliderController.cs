using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProniaMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        IWebHostEnvironment _environment;
        public SliderController(ISliderService sliderService, IWebHostEnvironment environment)
        {
            _sliderService = sliderService;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var sliders = _sliderService.GetAllSliders();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {

            if (!slider.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("PhotoFile", "Formati duzgun daxil edin");
                return View();
            }


            string filename = slider.ImageFile.FileName;
            string path = _environment.WebRootPath + @"\Upload\" + filename;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                slider.ImageFile.CopyTo(stream);

            }


            slider.ImageUrl = filename;


            if (!ModelState.IsValid)
            {
                return View();
            }
           _sliderService.AddSlider(slider);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var slider = _sliderService.GetAllSliders().FirstOrDefault(s => s.Id == id);
            string path = _environment.WebRootPath + @"\Upload\" + slider.ImageUrl;

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            _sliderService.DeleteSlider(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            var slider = _sliderService.GetAllSliders().FirstOrDefault(x => x.Id == id);
            if (slider == null)
            {
                return RedirectToAction("Index");
            }
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Slider newSlider)
        {
            var oldslider = _sliderService.GetAllSliders().FirstOrDefault(x => x.Id == newSlider.Id);
            if (oldslider == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                return View(newSlider);
            }
            if (newSlider.ImageFile != null && oldslider.ImageUrl != newSlider.ImageUrl)
            {
                var fileName=oldslider.ImageFile.FileName;
                var filePath =_environment.WebRootPath+@"\Upload\"+fileName;
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    newSlider.ImageFile.CopyTo(fileStream);
                }
                oldslider.ImageUrl =fileName;
            }
            oldslider.Title = newSlider.Title;
            oldslider.SubTitle = newSlider.SubTitle;
            oldslider.Description = newSlider.Description;

            _sliderService.UpdateSlider(newSlider.Id, newSlider);
            return RedirectToAction(nameof(Index));
        }


    }
}
