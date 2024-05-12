using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.RepositoryConcretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{

    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        public void AddSlider(Slider slider)
        {
            _sliderRepository.Add(slider);
            _sliderRepository.Commit();
        }

        public void DeleteSlider(int id)
        {
            var existslider = _sliderRepository.Get(x => x.Id == id);
            if (existslider == null) throw new NullReferenceException("Slider yoxdu");
            _sliderRepository.Delete(existslider);
            _sliderRepository.Commit();
        }
        public void UpdateSlider(int id, Slider newslider)
        {
            var existslider = _sliderRepository.Get(x => x.Id == id);
            if (existslider == null) throw new NullReferenceException("Slider yoxdu");
            existslider.Title = newslider.Title;
            existslider.SubTitle = newslider.SubTitle;
            existslider.Description = newslider.Description;
            if(newslider.ImageFile != null)
            {
                existslider.ImageUrl = newslider.ImageUrl;
                existslider.ImageFile = newslider.ImageFile;
            }
            _sliderRepository.Commit();
        }

        public List<Slider> GetAllSliders(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.GetAll(func);
        }

        public Slider GetSlider(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.Get(func);
        }

    }
}
