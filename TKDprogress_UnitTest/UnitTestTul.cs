using Moq;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
using TKDprogress_SL.Entities;
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
            List<TulDto> tuls = new()
            {
                new TulDto { Id = 1, Name = "Chun ji", Description = "Dit is een tul", Movements = new List<MovementDto>(),},
                new TulDto { Id = 2, Name = "Dan-Gun", Description = "Dit is een tul", Movements = new List<MovementDto>(),},
                new TulDto { Id = 3, Name = "Do-San", Description = "Dit is een tul", Movements = new List<MovementDto>(),},
            };

            _tulTestRepository.InitializeTuls(tuls);
        }

        [Test]
        public async Task GetTuls_ShouldGetTulsFromRepository()
        {
            // Arrange
            InitializeTulItems();

            // Act
            List<TulDto> tuls = await _tulService.GetTulsAsync("");

            // Assert
            Assert.That(tuls, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task CreateTul_ShouldAddTulToRepository()
        {
            // Arrange
            InitializeTulItems();

            TulDto tul = new()
            {
                Id = 4,
                Name = "Won-Hyo",
                Description = "Dit is een tul",
                Movements = new List<MovementDto>(),
            };

            // Act
            await _tulService.CreateTulAsync(tul);
            TulDto tulDto = _tulTestRepository.GetTulById(4);

            // Assert
            Assert.That(tulDto.Id, Is.EqualTo(4));
        }

        [Test]
        public async Task UpdateTul_ShouldUpdateTulInRepository()
        {
            // Arrange
            InitializeTulItems();

            TulDto newtul = new()
            {
                Id = 1,
                Name = "CHOONG-MOO",
                Description = "Dit is een tul",
                Movements = new List<MovementDto>(),
            };

            // Act
            await _tulService.UpdateTulAsync(newtul);

            TulDto tulDto = _tulTestRepository.GetTulById(1);

            // Assert
            Assert.That(tulDto, Is.EqualTo(newtul));
        }

        [Test]
        public async Task DeleteTul_ShouldDeleteTulInRepository()
        {
            // Arrange
            InitializeTulItems();

            TulDto tul = new()
            {
                Id = 1,
                Name = "Chun ji",
                Description = "Dit is een tul",
                Movements = new List<MovementDto>(),
            };

            // Act
            await _tulService.DeleteTulAsync(tul);
            TulDto tulDto = _tulTestRepository.GetTulById(1);

            // Assert
            Assert.That(tulDto.Id, Is.EqualTo(0));
        }
    }
}