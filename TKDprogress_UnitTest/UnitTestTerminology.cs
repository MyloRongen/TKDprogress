using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Services;
using TKDprogress_UnitTest.DummyRepositories;

namespace TKDprogress_UnitTest
{
    public class TerminologyTests
    {
        private TerminologyService _terminologyService;
        private TerminologyTestRepository _terminologyTestRepository;

        [SetUp]
        public void Setup()
        {
            _terminologyTestRepository = new TerminologyTestRepository();
            _terminologyService = new TerminologyService(_terminologyTestRepository);
        }

        private void InitializeTerminologyItems()
        {
            List<Terminology> terminologies = new()
            {
                new Terminology { Id = 1, Word = "Ye Ui", Meaning = "Hoffelijk", CategoryId = 1 },
                new Terminology { Id = 2, Word = "Ap", Meaning = "Voorwaards", CategoryId = 1 }, 
                new Terminology { Id = 3, Word = "Yop", Meaning = "Zijwaarts", CategoryId = 1 },
            };

            _terminologyTestRepository.InitializeTerminologies(terminologies);
        }

        [Test]
        public async Task GetTerminologies_ShouldGetTerminologiesFromRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            // Act
            List<Terminology> terminologies = await _terminologyService.GetTerminologiesAsync("");

            // Assert
            Assert.That(terminologies, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetTerminologyById_ShouldGetTerminologyByIdFromRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            // Act
            Terminology terminology = await _terminologyService.GetTerminologyByIdAsync(3);

            // Assert
            Assert.That(terminology.Id, Is.EqualTo(3));
        }

        [Test]
        public async Task CreateTerminology_ShouldAddTerminologyToRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            Terminology terminology = new()
            {
                Id = 4,
                Word = "Ye Ui",
                Meaning = "Hoffelijk",
                CategoryId = 1,
            };

            // Act
            await _terminologyService.CreateTerminologyAsync(terminology);
            Terminology newTerminology = await _terminologyService.GetTerminologyByIdAsync(4);

            // Assert
            Assert.That(newTerminology.Id, Is.EqualTo(4));
        }

        [Test]
        public async Task UpdateTerminology_ShouldUpdateTerminologyInRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            Terminology newterminology = new()
            {
                Id = 1,
                Word = "Yom Chi",
                Meaning = "Hoffelijk",
                CategoryId = 1,
            };

            // Act
            await _terminologyService.UpdateTerminologyAsync(newterminology);

            Terminology newTerminology = await _terminologyService.GetTerminologyByIdAsync(1);

            // Assert
            Assert.That(newTerminology, Is.EqualTo(newterminology));
        }

        [Test]
        public async Task DeleteTerminology_ShouldDeleteTerminologyInRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            Terminology terminology = new()
            {
                Id = 1,
                Word = "Ye Ui",
                Meaning = "Hoffelijk",
                CategoryId = 1,
            };

            // Act
            await _terminologyService.DeleteTerminologyAsync(terminology);
            Terminology newTerminology = await _terminologyService.GetTerminologyByIdAsync(1);

            // Assert
            Assert.That(newTerminology.Id, Is.EqualTo(0));
        }
    }
}