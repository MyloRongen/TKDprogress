using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Enums;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress_BLL.Services
{
    public class PercentageCalculationService : IPercentageCalculationService
    {
        public Task<(float unlearnedPercentage, float inProgressPercentage, float learnedPercentage)> CalculateTulPercentagesAsync(List<UserTul> allStatuses)
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

        public Task<(float unlearnedPercentage, float inProgressPercentage, float learnedPercentage)> CalculateCategoryPercentagesAsync(List<UserCategory> allStatuses)
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
