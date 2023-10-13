using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress_BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetCategoriesAsync(string searchString)
        {
            List<Category> categories = await _categoryRepository.GetCategoriesAsync(searchString);

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Category { ErrorMessage = "Invalid category." };
            }

            Category category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category.Id <= 0)
            {
                return new Category { ErrorMessage = "Category not found." };
            }

            return category;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (string.IsNullOrEmpty(category.Name) || string.IsNullOrEmpty(category.Description))
            {
                category.ErrorMessage =  "Category name or description has an incorrect input.";
                return category;
            }

            category = await _categoryRepository.CreateCategoryAsync(category);

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
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

        public async Task<Category> DeleteCategoryAsync(Category category)
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
