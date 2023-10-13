using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress_BLL.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<List<UserTul>> GetAllTulStatuses(string userId)
        {
            return await _statusRepository.GetAllTulStatuses(userId);
        }

        public async Task<List<UserCategory>> GetAllCategoryStatuses(string userId)
        {
            return await _statusRepository.GetAllCategoryStatuses(userId);
        }
    }
}
