using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_BLL.Interfaces
{
    public interface IPercentageCalculationService
    {
        Task<(float unlearnedPercentage, float inProgressPercentage, float learnedPercentage)> CalculateTulPercentagesAsync(List<UserTulDto> allStatuses);
        Task<(float unlearnedPercentage, float inProgressPercentage, float learnedPercentage)> CalculateCategoryPercentagesAsync(List<UserCategoryDto> allStatuses);
    }
}
