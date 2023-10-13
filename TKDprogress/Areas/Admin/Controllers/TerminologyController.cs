using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TKDprogress.Models;
using TKDprogress.Models.CreateModels;
using TKDprogress.Models.UpdateModels;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Interfaces.Services;
using TKDprogress_BLL.Services;

namespace TKDprogress.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TerminologyController : Controller
    {
        private readonly ITerminologyService _terminologyService;
        private readonly ICategoryService _categoryService;

        public TerminologyController(ITerminologyService terminologyService, ICategoryService categoryService)
        {
            _terminologyService = terminologyService;
            _categoryService = categoryService;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            List<Terminology> terminologies = await _terminologyService.GetTerminologiesAsync(searchString);

            if (terminologies.Any(t => t.ErrorMessage != null))
            {
                foreach (Terminology terminology in terminologies)
                {
                    if (terminology.ErrorMessage != null)
                    {
                        TempData["ErrorMessage"] = terminology.ErrorMessage;
                    }
                }

                return View(new List<TulViewModel>());
            }

            List<TerminologyViewModel> terminologyViewModels = terminologies.Select(terminology => new TerminologyViewModel
            {
                Id = terminology.Id,
                Word = terminology.Word,
                Meaning = terminology.Meaning,
                Category = ConvertToCategoryViewModel(terminology.Category),
            }).ToList();

            return View(terminologyViewModels);
        }

        public async Task<ActionResult> Details(int id)
        {
            Terminology terminology = await _terminologyService.GetTerminologyByIdAsync(id);

            if (terminology.ErrorMessage == null)
            {
                TerminologyViewModel terminologyViewModel = new()
                {
                    Id = terminology.Id,
                    Word = terminology.Word,
                    Meaning = terminology.Meaning,
                    Category = ConvertToCategoryViewModel(terminology.Category),
                };

                return View(terminologyViewModel);
            }

            TempData["ErrorMessage"] = terminology.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Create()
        {
            List<Category> categories = await _categoryService.GetCategoriesAsync("");

            List<CategoryViewModel> categoryViewModels = categories.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            }).ToList();

            CreateTerminologyViewModel terminologyViewModel = new()
            {
                Categories = categoryViewModels
            };

            return View(terminologyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTerminologyViewModel terminologyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(terminologyViewModel);
            }

            Terminology terminology = new()
            {
                Word = terminologyViewModel.Word,
                Meaning = terminologyViewModel.Meaning,
                CategoryId = terminologyViewModel.CategoryId,
            };

            terminology = await _terminologyService.CreateTerminologyAsync(terminology);

            if (terminology.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = terminology.ErrorMessage;
                return View(terminologyViewModel);
            }

            TempData["StatusMessage"] = "The terminology was successfully created!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            Terminology terminology = await _terminologyService.GetTerminologyByIdAsync(id);
            List<Category> categories = await _categoryService.GetCategoriesAsync("");

            List<CategoryViewModel> categorieViewModels = categories.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            }).ToList();

            UpdateTerminologyViewModel terminologyViewModel = new()
            {
                Id = terminology.Id,
                Word = terminology.Word,
                Meaning = terminology.Meaning,
                CategoryId= terminology.CategoryId,
                Categories = categorieViewModels,
            };

            return View(terminologyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateTerminologyViewModel terminologyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(terminologyViewModel);
            }

            Terminology terminology = new()
            {
                Id = terminologyViewModel.Id,
                Word = terminologyViewModel.Word,
                Meaning = terminologyViewModel.Meaning,
                CategoryId = terminologyViewModel.CategoryId,
            };

            terminology = await _terminologyService.UpdateTerminologyAsync(terminology);

            if (terminology.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = terminology.ErrorMessage;
                return View(terminologyViewModel);
            }

            TempData["StatusMessage"] = "The terminology was successfully updated!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            Terminology terminology = await _terminologyService.GetTerminologyByIdAsync(id);

            TerminologyViewModel terminologyViewModel = new()
            {
                Id = terminology.Id,
                Word = terminology.Word,
                Meaning = terminology.Meaning,
                Category = ConvertToCategoryViewModel(terminology.Category),
            };

            return View(terminologyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                Terminology terminology = await _terminologyService.GetTerminologyByIdAsync(id);
                await _terminologyService.DeleteTerminologyAsync(terminology);

                TempData["StatusMessage"] = "The terminology was successfully deleted!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return View();
            }
        }

        private static CategoryViewModel ConvertToCategoryViewModel(Category category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}
