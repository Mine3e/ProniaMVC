using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public  interface ICategoryService
    {
        void AddCategory(Category category);
        void DeleteCategory(int id);    
        void UpdateCategory(int id, Category newcategory);
        Category GetCategory(Func<Category, bool>? func = null);
        List<Category> GetAllCategories(Func<Category, bool>? func = null);
    }
}
