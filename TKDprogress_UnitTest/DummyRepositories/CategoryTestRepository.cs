using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_UnitTest.DummyRepositories
{
    public class CategoryTestRepository : ICategoryRepository
    {
        private List<CategoryDto> categories;

        public void InitializeCategories(List<CategoryDto> newCategories)
        {
            categories = newCategories;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync(string searchString)
        {
            return categories;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            CategoryDto category = new();

            foreach (var item in categories)
            {
                if (item.Id == id)
                {
                    category = item;
                    break;
                }
            }

            return category;
        }


        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto category)
        {
            foreach (var item in categories)
            {
                if (category.Id == item.Id)
                {
                    throw new Exception("This category already exists!");
                }
            }

            categories.Add(category);
            return category;
        }

        public async Task<CategoryDto> DeleteCategoryAsync(CategoryDto category)
        {
            foreach (var item in categories)
            {
                if (category.Id == item.Id)
                {
                    categories.Remove(item);
                    break;
                }
            }

            return category;
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto newCategory)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (newCategory.Id == categories[i].Id)
                {
                    categories[i] = newCategory;
                    break;
                }
            }

            return newCategory;
        }
    }
}
