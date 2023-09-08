using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TKDprogress.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
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
/*            try
            {*/
                List<CategoryDto> categories = await _categoryService.GetCategoriesAsync(searchString);

                List<CategoryViewModel> categoryViewModels = categories.Select(category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                }).ToList();

                return View(categoryViewModels);
/*            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching category index.";
                return View();
            }*/
        }

        public async Task<ActionResult> Details(int id)
        {
            try
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
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching category details: " + ex.Message;
                return View();
            }
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

            _ = await _categoryService.CreateCategoryAsync(category);

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

            _ = await _categoryService.UpdateCategoryAsync(category);

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
