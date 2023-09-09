using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TKDprogress.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Enums;

namespace TKDprogress.Controllers
{
    public class TulController : Controller
    {
        private readonly IUserTulService _userTulService;
        private readonly ITulMovementService _tulMovementService;

        public TulController(IUserTulService userTulService, ITulMovementService tulMovementService)
        {
            _userTulService = userTulService;
            _tulMovementService = tulMovementService;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<UserTulDto> userTuls = await _userTulService.GetTulsAssignedToUserAsync(userId, searchString);

            List<UserTulViewModel> userTulViewModels = userTuls.Select(userTul => new UserTulViewModel
            {
                Id = userTul.Tul.Id,
                Name = userTul.Tul.Name,
                Status = userTul.Status,
                StatusText = EnumStatusToText(userTul.Status)
            }).ToList();

            return View(userTulViewModels);
        }

        public async Task<ActionResult> Details(int tulId)
        {
            TulDto tul = await _tulMovementService.GetTulWithMovementByIdAsync(tulId);

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserTulDto userTul = await _userTulService.GetUserTul(tulId, userId);

            if (tul != null)
            {
                TulViewModel tulViewModel = new()
                {
                    Id = tul.Id,
                    Name = tul.Name,
                    Description = tul.Description,
                    Status = userTul?.Status,
                    Movements = tul.Movements.Select(movement => new MovementDto
                    {
                        Id = movement.Id,
                        Name = movement.Name,
                        ImageUrl = movement.ImageUrl,
                    }).ToList(),
                };

                return View(tulViewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Tul doesn't contain Movements!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUserTulStatus(int id, EnumStatus newStatus)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserTulDto userTul = await _userTulService.GetUserTul(id, userId);

            if (userTul != null)
            {
                userTul.Status = newStatus;
                await _userTulService.UpdateUserTulStatus(userTul);

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
