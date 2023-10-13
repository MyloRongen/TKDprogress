using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Models;

namespace TKDprogress_UnitTest.DummyRepositories
{
    public class CategoryTestRepository : ICategoryRepository
    {
        private List<Category> categories;

        public void InitializeCategories(List<Category> newCategories)
        {
            categories = newCategories;
        }

        public async Task<List<Category>> GetCategoriesAsync(string searchString)
        {
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            Category category = new();

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


        public async Task<Category> CreateCategoryAsync(Category category)
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

        public async Task<Category> DeleteCategoryAsync(Category category)
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

        public async Task<Category> UpdateCategoryAsync(Category newCategory)
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
