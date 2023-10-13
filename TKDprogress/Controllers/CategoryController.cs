using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TKDprogress.Models;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Enums;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Interfaces.Services;
using TKDprogress_BLL.Services;

namespace TKDprogress.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUserCategoryService _userCategoryService;
        private readonly ICategoryTerminologyService _categoryTerminologyService;

        public CategoryController(IUserCategoryService userCategoryService, ICategoryTerminologyService categoryTerminologyService)
        {
            _userCategoryService = userCategoryService;
            _categoryTerminologyService = categoryTerminologyService;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<UserCategory> categories = await _userCategoryService.GetCategoriesAssignedToUserAsync(userId, searchString);

            List<UserCategoryViewModel> categoryViewModels = categories.Select(category => new UserCategoryViewModel
            {
                Id = category.Category.Id,
                Name = category.Category.Name, 
                Status = category.Status,
                StatusText = EnumStatusToText(category.Status)
            }).ToList();

            return View(categoryViewModels);
        }

        public async Task<ActionResult> Details(int categoryId)
        {
            Category category = await _categoryTerminologyService.GetTerminologiesAssignedToCategoryAsync(categoryId);

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserCategory userCategory = await _userCategoryService.GetUserCategory(categoryId, userId);

            if (category != null)
            {
                CategoryViewModel categoryViewModel = new()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    Status = userCategory?.Status,
                    Terminologies = category.Terminologies.Select(terminology => new TerminologyViewModel
                    {
                        Id = terminology.Id,
                        Word = terminology.Word,
                        Meaning = terminology.Meaning,
                    }).ToList(),
                };

                return View(categoryViewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Category doesn't contain terminologies!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUserCategoryStatus(int id, EnumStatus newStatus)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserCategory userCategory = await _userCategoryService.GetUserCategory(id, userId);

            if (userCategory != null)
            {
                userCategory.Status = newStatus;
                await _userCategoryService.UpdateUserCategoryStatus(userCategory);

                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "An error occurred while processing your request.";
            return View();
        }

        private static string EnumStatusToText(EnumStatus status)
        {
            return status switch
            {
                EnumStatus.unlearned => "unlearned",
                EnumStatus.inProgress => "in progress",
                EnumStatus.learned => "learned",
                _ => "No status",
            };
        }
    }
}
