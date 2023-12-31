using Moq;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Services;
using TKDprogress_UnitTest.DummyRepositories;

namespace TKDprogress_UnitTest
{
    public class MovementTests
    {
        private MovementService _movementService;
        private MovementTestRepository _movementTestRepository;

        [SetUp]
        public void Setup()
        {
            _movementTestRepository = new MovementTestRepository();
            _movementService = new MovementService(_movementTestRepository);
        }

        private void InitializeMovementItems()
        {
            List<Movement> movements = new()
            {
                new Movement { Id = 1, Name = "Ready stance", ImageUrl = "test.jpg"},
                new Movement { Id = 2, Name = "Right low block", ImageUrl = "test.jpg"},
                new Movement { Id = 3, Name = "Left low block", ImageUrl = "test.jpg"},
            };

            _movementTestRepository.InitializeMovements(movements);
        }

        [Test]
        public async Task GetMovements_ShouldGetMovementsFromRepository()
        {
            // Arrange
            InitializeMovementItems();

            // Act
            List<Movement> movements = await _movementService.GetMovementsAsync("");

            // Assert
            Assert.That(movements, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetMovementById_ShouldGetMovementByIdFromRepository()
        {
            // Arrange
            InitializeMovementItems();

            // Act
            Movement movement = await _movementService.GetMovementByIdAsync(3);

            // Assert
            Assert.That(movement.Id, Is.EqualTo(3));
        }

        [Test]
        public async Task CreateMovement_ShouldAddMovementToRepository()
        {
            // Arrange
            InitializeMovementItems();

            Movement movement = new()
            {
                Id = 4,
                Name = "Punch forward",
                ImageUrl = "test.jpg",
            };

            // Act
            await _movementService.CreateMovementAsync(movement);
            Movement newMovement = await _movementTestRepository.GetMovementByIdAsync(4);

            // Assert
            Assert.That(newMovement.Id, Is.EqualTo(4));
        }

        [Test]
        public async Task UpdateMovement_ShouldUpdateMovementInRepository()
        {
            // Arrange
            InitializeMovementItems();

            Movement newmovement = new()
            {
                Id = 1,
                Name = "Right low block",
                ImageUrl = "test.jpg",
            };

            // Act
            await _movementService.UpdateMovementAsync(newmovement);

            Movement newMovement = await _movementTestRepository.GetMovementByIdAsync(1);

            // Assert
            Assert.That(newMovement, Is.EqualTo(newmovement));
        }

        [Test]
        public async Task DeleteMovement_ShouldDeleteMovementInRepository()
        {
            // Arrange
            InitializeMovementItems();

            Movement movement = new()
            {
                Id = 1,
                Name = "Ready stance",
                ImageUrl = "test.jpg",
            };

            // Act
            await _movementService.DeleteMovementAsync(movement);
            Movement newMovement = await _movementTestRepository.GetMovementByIdAsync(1);

            // Assert
            Assert.That(newMovement.Id, Is.EqualTo(0));
        }
    }
}