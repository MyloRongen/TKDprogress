using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Services;
using TKDprogress_UnitTest.DummyRepositories;

namespace TKDprogress_UnitTest
{
    public class CategoryTests
    {
        private CategoryService _categoryService;
        private CategoryTestRepository _categoryTestRepository;

        [SetUp]
        public void Setup()
        {
            _categoryTestRepository = new CategoryTestRepository();
            _categoryService = new CategoryService(_categoryTestRepository);
        }

        private void InitializeCategoryItems()
        {
            List<Category> categories = new()
            {
                new Category { Id = 1, Name = "Junshin", Description = "Dit is een tul", Terminologies = new List<Terminology>()},
                new Category { Id = 2, Name = "Him Ui Wolli", Description = "Dit is een tul", Terminologies = new List<Terminology>()},
                new Category { Id = 3, Name = "Commando’s", Description = "Dit is een tul", Terminologies = new List<Terminology>()},
            };

            _categoryTestRepository.InitializeCategories(categories);
        }

        [Test]
        public async Task GetCategories_ShouldGetCategoriesFromRepository()
        {
            // Arrange
            InitializeCategoryItems();

            // Act
            List<Category> categories = await _categoryService.GetCategoriesAsync("");

            // Assert
            Assert.That(categories, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetCategoryById_ShouldGetCategoryByIdFromRepository()
        {
            // Arrange
            InitializeCategoryItems();

            // Act
            Category category = await _categoryService.GetCategoryByIdAsync(3);

            // Assert
            Assert.That(category.Id, Is.EqualTo(3));
        }

        [Test]
        public async Task CreateCategory_ShouldAddCategoryToRepository()
        {
            // Arrange
            InitializeCategoryItems();

            Category category = new()
            {
                Id = 4,
                Name = "CHOONG-MOO",
                Description = "Dit is een tul",
                Terminologies = new List<Terminology>(),
            };

            // Act
            await _categoryService.CreateCategoryAsync(category);
            Category newCategory = await _categoryService.GetCategoryByIdAsync(4);

            // Assert
            Assert.That(newCategory.Id, Is.EqualTo(4));
        }

        [Test]
        public async Task UpdateCategory_ShouldUpdateCategoryInRepository()
        {
            // Arrange
            InitializeCategoryItems();

            Category newcategory = new()
            {
                Id = 1,
                Name = "CHOONG-MOO",
                Description = "Dit is een tul",
                Terminologies = new List<Terminology>(),
            };

            // Act
            await _categoryService.UpdateCategoryAsync(newcategory);

            Category category = await _categoryService.GetCategoryByIdAsync(1);

            // Assert
            Assert.That(category, Is.EqualTo(newcategory));
        }

        [Test]
        public async Task DeleteCategory_ShouldDeleteCategoryInRepository()
        {
            // Arrange
            InitializeCategoryItems();

            Category category = new()
            {
                Id = 1,
                Name = "Junshin",
                Description = "Dit is een tul",
                Terminologies = new List<Terminology>(),
            };

            // Act
            await _categoryService.DeleteCategoryAsync(category);
            Category deletedCategory = await _categoryService.GetCategoryByIdAsync(1);

            // Assert
            Assert.That(deletedCategory.Id, Is.EqualTo(0));
        }
    }
}