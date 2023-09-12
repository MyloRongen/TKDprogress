using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync(string searchString)
        {
            List<CategoryDto> categories = await _categoryRepository.GetCategoriesAsync(searchString);

            return categories;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new CategoryDto { ErrorMessage = "Invalid category." };
            }

            CategoryDto category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category.Id <= 0)
            {
                return new CategoryDto { ErrorMessage = "Category not found." };
            }

            return category;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto category)
        {
            if (string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.Description))
            {
                category.ErrorMessage =  "Category name or description has an incorrect input.";
                return category;
            }

            category = await _categoryRepository.CreateCategoryAsync(category);

            return category;
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto category)
        {
            if (category.Id <= 0)
            {
                category.ErrorMessage = "Category type does not exist.";
                return category;
            }

            if (string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.Description))
            {
                category.ErrorMessage = "Category name or description has an incorrect input.";
                return category;
            }

            category = await _categoryRepository.UpdateCategoryAsync(category);

            return category;
        }

        public async Task<CategoryDto> DeleteCategoryAsync(CategoryDto category)
        {
            if (category.Id <= 0)
            {
                category.ErrorMessage = "Category type does not exist.";
                return category;
            }

            await _categoryRepository.DeleteCategoryAsync(category);

            return category;
        }
    }
}
