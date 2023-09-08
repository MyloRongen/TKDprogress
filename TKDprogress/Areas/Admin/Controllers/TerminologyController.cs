using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TKDprogress.Models;
using TKDprogress.Models.CreateModels;
using TKDprogress.Models.UpdateModels;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
using TKDprogress_SL.Entities;

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
            List<TerminologyDto> terminologies = await _terminologyService.GetTerminologiesAsync(searchString);

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
            try
            {
                TerminologyDto terminology = await _terminologyService.GetTerminologyByIdAsync(id);

                TerminologyViewModel terminologyViewModel = new()
                {
                    Id = terminology.Id,
                    Word = terminology.Word,
                    Meaning = terminology.Meaning,
                    Category = ConvertToCategoryViewModel(terminology.Category),
                };

                return View(terminologyViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching category details: " + ex.Message;
                return View();
            }
        }

        public async Task<ActionResult> Create()
        {
            List<CategoryDto> categories = await _categoryService.GetCategoriesAsync("");

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

            TerminologyDto terminology = new()
            {
                Word = terminologyViewModel.Word,
                Meaning = terminologyViewModel.Meaning,
                CategoryId = terminologyViewModel.CategoryId,
            };

            _ = await _terminologyService.CreateTerminologyAsync(terminology);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            TerminologyDto terminology = await _terminologyService.GetTerminologyByIdAsync(id);
            List<CategoryDto> categories = await _categoryService.GetCategoriesAsync("");

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

            TerminologyDto terminology = new()
            {
                Id = terminologyViewModel.Id,
                Word = terminologyViewModel.Word,
                Meaning = terminologyViewModel.Meaning,
                CategoryId = terminologyViewModel.CategoryId,
            };

            _ = await _terminologyService.UpdateTerminologyAsync(terminology);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            TerminologyDto terminology = await _terminologyService.GetTerminologyByIdAsync(id);

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
            TerminologyDto terminology = await _terminologyService.GetTerminologyByIdAsync(id);
            await _terminologyService.DeleteTerminologyAsync(terminology);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return View();
            }
        }

        private static CategoryViewModel ConvertToCategoryViewModel(CategoryDto category)
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
