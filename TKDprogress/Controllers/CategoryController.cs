using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TKDprogress.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Enums;

namespace TKDprogress.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUserCategoryService _userCategoryService;

        public CategoryController(IUserCategoryService userCategoryService)
        {
            _userCategoryService = userCategoryService;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<UserCategoryDto> categories = await _userCategoryService.GetCategoriesAssignedToUserAsync(userId, searchString);

            List<UserCategoryViewModel> categoryViewModels = categories.Select(category => new UserCategoryViewModel
            {
                Id = category.Category.Id,
                Name = category.Category.Name, 
                Status = category.Status,
                StatusText = EnumStatusToText(category.Status)
            }).ToList();

            return View(categoryViewModels);
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
