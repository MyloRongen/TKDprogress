using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TKDprogress.Models;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IStatusService _statusService;
        private readonly IPercentageCalculationService _percentageCalculationService;

        public DashboardController(IStatusService statusService, IPercentageCalculationService percentageCalculationService)
        {
            _statusService = statusService;
            _percentageCalculationService = percentageCalculationService;
        }

        public async Task<ActionResult> Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            StatusTulViewModel statusTulViewModel = await CalculateStatusTulViewModelAsync(userId);
            StatusCategoryViewModel statusCategoryViewModel = await CalculateStatusCategoryViewModelAsync(userId);

            StatusTulCategoryViewModel combinedViewModel = new()
            {
                StatusTul = statusTulViewModel,
                StatusCategory = statusCategoryViewModel
            };

            return View(combinedViewModel);
        }

        public async Task<StatusTulViewModel> CalculateStatusTulViewModelAsync(string userId)
        {
            List<UserTul> allTulStatuses = await _statusService.GetAllTulStatuses(userId);
            var (unlearnedTulPercentage, inProgressTulPercentage, learnedTulPercentage) = await _percentageCalculationService.CalculateTulPercentagesAsync(allTulStatuses);

            return new StatusTulViewModel
            {
                UnlearnedPercentage = unlearnedTulPercentage,
                InProgressPercentage = inProgressTulPercentage,
                LearnedPercentage = learnedTulPercentage
            };
        }

        public async Task<StatusCategoryViewModel> CalculateStatusCategoryViewModelAsync(string userId)
        {
            List<UserCategory> allCategoryStatuses = await _statusService.GetAllCategoryStatuses(userId);
            var (unlearnedCategoryPercentage, inProgressCategoryPercentage, learnedCategoryPercentage) = await _percentageCalculationService.CalculateCategoryPercentagesAsync(allCategoryStatuses);

            return new StatusCategoryViewModel
            {
                UnlearnedPercentage = unlearnedCategoryPercentage,
                InProgressPercentage = inProgressCategoryPercentage,
                LearnedPercentage = learnedCategoryPercentage
            };
        }
    }
}
