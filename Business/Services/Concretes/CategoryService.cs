using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void AddCategory(Category category)
        {
            if (category == null) throw new NullReferenceException("Category null ola bilmez");
            if(!_categoryRepository.GetAll().Any(x=>x.Name == category.Name))
            {
                _categoryRepository.Add(category);
                _categoryRepository.Commit();
            }
            else
            {
                throw new DuplicateCategoryException("Name", "Category adi eyni ola bilmez");
            }
        }

        public void DeleteCategory(int id)
        {
           var existCategory=_categoryRepository.Get(x=>x.Id == id);
           if (existCategory == null)  throw new NullReferenceException("Category yoxdu");
           _categoryRepository.Delete(existCategory);
           _categoryRepository.Commit() ;
        }

        public List<Category> GetAllCategories(Func<Category, bool>? func = null)
        {
            return _categoryRepository.GetAll(func);
        }

        public Category GetCategory(Func<Category, bool>? func = null)
        {
            return _categoryRepository.Get(func);
        }

        public void UpdateCategory(int id, Category newcategory)
        {
            var existCategory = _categoryRepository.Get(x => x.Id == id);
            if (existCategory == null) throw new NullReferenceException("Category yoxdu");
            if (!_categoryRepository.GetAll().Any(x => x.Name == newcategory.Name))
            {
                existCategory.Name= newcategory.Name;
                _categoryRepository.Commit();
            }
            else
            {
                throw new DuplicateCategoryException("Name","Category adi eyni ola bilmez");
            }
        }
    }
}
