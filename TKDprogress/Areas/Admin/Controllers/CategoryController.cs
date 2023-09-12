using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TKDprogress.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
using TKDprogress_DAL.Entities;
using TKDprogress_DAL.Repositories;
using TKDprogress_SL.Entities;

namespace TKDprogress.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ActionResult> IndexAsync(string searchString)
        {
            List<CategoryDto> categories = await _categoryService.GetCategoriesAsync(searchString);

            if (categories.Any(c => c.ErrorMessage != null))
            {
                foreach (CategoryDto category in categories)
                {
                    if (category.ErrorMessage != null)
                    {
                        TempData["ErrorMessage"] = category.ErrorMessage;
                    }
                }

                return View(new List<CategoryViewModel>());
            }

            List<CategoryViewModel> categoryViewModels = categories.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            }).ToList();

            return View(categoryViewModels);
        }

        public async Task<ActionResult> Details(int id)
        {
            CategoryDto category = await _categoryService.GetCategoryByIdAsync(id);

            if (category.ErrorMessage == null)
            {
                CategoryViewModel categoryViewModel = new()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                };

                return View(categoryViewModel);
            }

            TempData["ErrorMessage"] = category.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            CategoryDto category = new()
            {
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description
            };

            category = await _categoryService.CreateCategoryAsync(category);

            if (category.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = category.ErrorMessage;
                return View(categoryViewModel);
            }

            TempData["StatusMessage"] = "The category was successfully created!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            CategoryDto category = await _categoryService.GetCategoryByIdAsync(id);

            UpdateCategoryViewModel categoryViewModel = new()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateCategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            CategoryDto category = new()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description,
            };

            category = await _categoryService.UpdateCategoryAsync(category);

            if (category.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = category.ErrorMessage;
                return View(categoryViewModel);
            }

            TempData["StatusMessage"] = "The category was successfully updated!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            CategoryDto category = await _categoryService.GetCategoryByIdAsync(id);

            CategoryViewModel categoryViewModel = new()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            CategoryDto category = await _categoryService.GetCategoryByIdAsync(id);
            await _categoryService.DeleteCategoryAsync(category);

            try
            {
                TempData["StatusMessage"] = "The category was successfully deleted!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return View();
            }
        }
    }
}
