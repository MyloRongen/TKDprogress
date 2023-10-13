using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface IPercentageCalculationService
    {
        Task<(float unlearnedPercentage, float inProgressPercentage, float learnedPercentage)> CalculateTulPercentagesAsync(List<UserTul> allStatuses);
        Task<(float unlearnedPercentage, float inProgressPercentage, float learnedPercentage)> CalculateCategoryPercentagesAsync(List<UserCategory> allStatuses);
    }
}
