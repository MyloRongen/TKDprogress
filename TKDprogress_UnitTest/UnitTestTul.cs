using Moq;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Services;
using TKDprogress_UnitTest.DummyRepositories;

namespace TKDprogress_UnitTest
{
    public class TulTests
    {
        private TulService _tulService;
        private TulTestRepository _tulTestRepository;

        [SetUp]
        public void Setup()
        {
            _tulTestRepository = new TulTestRepository();
            _tulService = new TulService(_tulTestRepository);
        }

        private void InitializeTulItems()
        {
            List<Tul> tuls = new()
            {
                new Tul { Id = 1, Name = "Chun ji", Description = "Dit is een tul", Movements = new List<Movement>(),},
                new Tul { Id = 2, Name = "Dan-Gun", Description = "Dit is een tul", Movements = new List<Movement>(),},
                new Tul { Id = 3, Name = "Do-San", Description = "Dit is een tul", Movements = new List<Movement>(),},
            };

            _tulTestRepository.InitializeTuls(tuls);
        }

        [Test]
        public async Task GetTuls_ShouldGetTulsFromRepository()
        {
            // Arrange
            InitializeTulItems();

            // Act
            List<Tul> tuls = await _tulService.GetTulsAsync("");

            // Assert
            Assert.That(tuls, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task CreateTul_ShouldAddTulToRepository()
        {
            // Arrange
            InitializeTulItems();

            Tul tul = new()
            {
                Id = 4,
                Name = "Won-Hyo",
                Description = "Dit is een tul",
                Movements = new List<Movement>(),
            };

            // Act
            await _tulService.CreateTulAsync(tul);
            Tul tulDto = _tulTestRepository.GetTulById(4);

            // Assert
            Assert.That(tulDto.Id, Is.EqualTo(4));
        }

        [Test]
        public async Task UpdateTul_ShouldUpdateTulInRepository()
        {
            // Arrange
            InitializeTulItems();

            Tul newtul = new()
            {
                Id = 1,
                Name = "CHOONG-MOO",
                Description = "Dit is een tul",
                Movements = new List<Movement>(),
            };

            // Act
            await _tulService.UpdateTulAsync(newtul);

            Tul tulDto = _tulTestRepository.GetTulById(1);

            // Assert
            Assert.That(tulDto, Is.EqualTo(newtul));
        }

        [Test]
        public async Task DeleteTul_ShouldDeleteTulInRepository()
        {
            // Arrange
            InitializeTulItems();

            Tul tul = new()
            {
                Id = 1,
                Name = "Chun ji",
                Description = "Dit is een tul",
                Movements = new List<Movement>(),
            };

            // Act
            await _tulService.DeleteTulAsync(tul);
            Tul tulDto = _tulTestRepository.GetTulById(1);

            // Assert
            Assert.That(tulDto.Id, Is.EqualTo(0));
        }
    }
}