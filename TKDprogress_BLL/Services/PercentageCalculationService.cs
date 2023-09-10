using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Enums;

namespace TKDprogress_BLL.Services
{
    public class PercentageCalculationService : IPercentageCalculationService
    {
        public Task<(float unlearnedPercentage, float inProgressPercentage, float learnedPercentage)> CalculateTulPercentagesAsync(List<UserTulDto> allStatuses)
        {
            int unlearnedCount = allStatuses.Count(status => status.Status == EnumStatus.unlearned);
            int inProgressCount = allStatuses.Count(status => status.Status == EnumStatus.inProgress);
            int learnedCount = allStatuses.Count(status => status.Status == EnumStatus.learned);

            int total = allStatuses.Count;

            float unlearnedPercentage = ((float)unlearnedCount / total) * 100;
            float inProgressPercentage = ((float)inProgressCount / total) * 100;
            float learnedPercentage = ((float)learnedCount / total) * 100;

            return Task.FromResult((unlearnedPercentage, inProgressPercentage, learnedPercentage));
        }

        public Task<(float unlearnedPercentage, float inProgressPercentage, float learnedPercentage)> CalculateCategoryPercentagesAsync(List<UserCategoryDto> allStatuses)
        {
            int unlearnedCount = allStatuses.Count(status => status.Status == EnumStatus.unlearned);
            int inProgressCount = allStatuses.Count(status => status.Status == EnumStatus.inProgress);
            int learnedCount = allStatuses.Count(status => status.Status == EnumStatus.learned);

            int total = allStatuses.Count;

            float unlearnedPercentage = ((float)unlearnedCount / total) * 100;
            float inProgressPercentage = ((float)inProgressCount / total) * 100;
            float learnedPercentage = ((float)learnedCount / total) * 100;

            return Task.FromResult((unlearnedPercentage, inProgressPercentage, learnedPercentage));
        }
    }
}
