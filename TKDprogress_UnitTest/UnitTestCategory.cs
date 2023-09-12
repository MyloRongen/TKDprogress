using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
using TKDprogress_SL.Entities;
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
            List<CategoryDto> categories = new()
            {
                new CategoryDto { Id = 1, Name = "Junshin", Description = "Dit is een tul", Terminologies = new List<TerminologyDto>()},
                new CategoryDto { Id = 2, Name = "Him Ui Wolli", Description = "Dit is een tul", Terminologies = new List<TerminologyDto>()},
                new CategoryDto { Id = 3, Name = "Commando’s", Description = "Dit is een tul", Terminologies = new List<TerminologyDto>()},
            };

            _categoryTestRepository.InitializeCategories(categories);
        }

        [Test]
        public async Task GetCategories_ShouldGetCategoriesFromRepository()
        {
            // Arrange
            InitializeCategoryItems();

            // Act
            List<CategoryDto> categories = await _categoryService.GetCategoriesAsync("");

            // Assert
            Assert.That(categories, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetCategoryById_ShouldGetCategoryByIdFromRepository()
        {
            // Arrange
            InitializeCategoryItems();

            // Act
            CategoryDto category = await _categoryService.GetCategoryByIdAsync(3);

            // Assert
            Assert.That(category.Id, Is.EqualTo(3));
        }

        [Test]
        public async Task CreateCategory_ShouldAddCategoryToRepository()
        {
            // Arrange
            InitializeCategoryItems();

            CategoryDto category = new()
            {
                Id = 4,
                Name = "CHOONG-MOO",
                Description = "Dit is een tul",
                Terminologies = new List<TerminologyDto>(),
            };

            // Act
            await _categoryService.CreateCategoryAsync(category);
            CategoryDto categoryDto = await _categoryService.GetCategoryByIdAsync(4);

            // Assert
            Assert.That(categoryDto.Id, Is.EqualTo(4));
        }

        [Test]
        public async Task UpdateCategory_ShouldUpdateCategoryInRepository()
        {
            // Arrange
            InitializeCategoryItems();

            CategoryDto newcategory = new()
            {
                Id = 1,
                Name = "CHOONG-MOO",
                Description = "Dit is een tul",
                Terminologies = new List<TerminologyDto>(),
            };

            // Act
            await _categoryService.UpdateCategoryAsync(newcategory);

            CategoryDto categoryDto = await _categoryService.GetCategoryByIdAsync(1);

            // Assert
            Assert.That(categoryDto, Is.EqualTo(newcategory));
        }

        [Test]
        public async Task DeleteCategory_ShouldDeleteCategoryInRepository()
        {
            // Arrange
            InitializeCategoryItems();

            CategoryDto category = new()
            {
                Id = 1,
                Name = "Junshin",
                Description = "Dit is een tul",
                Terminologies = new List<TerminologyDto>(),
            };

            // Act
            await _categoryService.DeleteCategoryAsync(category);
            CategoryDto categoryDto = await _categoryService.GetCategoryByIdAsync(1);

            // Assert
            Assert.That(categoryDto.Id, Is.EqualTo(0));
        }
    }
}