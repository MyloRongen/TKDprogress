using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync(string searchString);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category newCategory);
        Task<Category> DeleteCategoryAsync(Category category);
    }
}
