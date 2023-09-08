using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategoriesAsync(string searchString);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto category);
        Task<CategoryDto> UpdateCategoryAsync(CategoryDto newCategory);
        Task<CategoryDto> DeleteCategoryAsync(CategoryDto category);
    }
}
