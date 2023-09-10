using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_BLL.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<List<UserTulDto>> GetAllTulStatuses(string userId)
        {
            return await _statusRepository.GetAllTulStatuses(userId);
        }

        public async Task<List<UserCategoryDto>> GetAllCategoryStatuses(string userId)
        {
            return await _statusRepository.GetAllCategoryStatuses(userId);
        }
    }
}
