using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
using TKDprogress_SL.Entities;
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
            List<TerminologyDto> terminologies = new()
            {
                new TerminologyDto { Id = 1, Word = "Ye Ui", Meaning = "Hoffelijk", CategoryId = 1 },
                new TerminologyDto { Id = 2, Word = "Ap", Meaning = "Voorwaards", CategoryId = 1 }, 
                new TerminologyDto { Id = 3, Word = "Yop", Meaning = "Zijwaarts", CategoryId = 1 },
            };

            _terminologyTestRepository.InitializeTerminologies(terminologies);
        }

        [Test]
        public async Task GetTerminologies_ShouldGetTerminologiesFromRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            // Act
            List<TerminologyDto> terminologies = await _terminologyService.GetTerminologiesAsync("");

            // Assert
            Assert.That(terminologies, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetTerminologyById_ShouldGetTerminologyByIdFromRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            // Act
            TerminologyDto terminology = await _terminologyService.GetTerminologyByIdAsync(3);

            // Assert
            Assert.That(terminology.Id, Is.EqualTo(3));
        }

        [Test]
        public async Task CreateTerminology_ShouldAddTerminologyToRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            TerminologyDto terminology = new()
            {
                Id = 4,
                Word = "Ye Ui",
                Meaning = "Hoffelijk",
                CategoryId = 1,
            };

            // Act
            await _terminologyService.CreateTerminologyAsync(terminology);
            TerminologyDto terminologyDto = await _terminologyService.GetTerminologyByIdAsync(4);

            // Assert
            Assert.That(terminologyDto.Id, Is.EqualTo(4));
        }

        [Test]
        public async Task UpdateTerminology_ShouldUpdateTerminologyInRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            TerminologyDto newterminology = new()
            {
                Id = 1,
                Word = "Yom Chi",
                Meaning = "Hoffelijk",
                CategoryId = 1,
            };

            // Act
            await _terminologyService.UpdateTerminologyAsync(newterminology);

            TerminologyDto terminologyDto = await _terminologyService.GetTerminologyByIdAsync(1);

            // Assert
            Assert.That(terminologyDto, Is.EqualTo(newterminology));
        }

        [Test]
        public async Task DeleteTerminology_ShouldDeleteTerminologyInRepository()
        {
            // Arrange
            InitializeTerminologyItems();

            TerminologyDto terminology = new()
            {
                Id = 1,
                Word = "Ye Ui",
                Meaning = "Hoffelijk",
                CategoryId = 1,
            };

            // Act
            await _terminologyService.DeleteTerminologyAsync(terminology);
            TerminologyDto terminologyDto = await _terminologyService.GetTerminologyByIdAsync(1);

            // Assert
            Assert.That(terminologyDto.Id, Is.EqualTo(0));
        }
    }
}