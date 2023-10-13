using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress_BLL.Services
{
    public class UserTulService : IUserTulService
    {
        private readonly IUserTulRepository _userTulRepository;

        public UserTulService(IUserTulRepository userTulRepository)
        {
            _userTulRepository = userTulRepository;
        }

        public async Task<List<UserTul>> GetTulsAssignedToUserAsync(string? userId, string searchString)
        {
            List<UserTul> assignedtuls = await _userTulRepository
                .GetTulsAssignedToUserAsync(userId, searchString);

            return assignedtuls;
        }

        public async Task<UserTul> GetUserTul(int tulId, string userId)
        {
            return await _userTulRepository.GetUserTul(tulId, userId);
        }

        public async Task<UserTul> UpdateUserTulStatus(UserTul userTul)
        {
            return await _userTulRepository.UpdateUserTulStatus(userTul);
        }
    }
}
