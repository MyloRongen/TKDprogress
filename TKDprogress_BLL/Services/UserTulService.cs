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
    public class UserTulService : IUserTulService
    {
        private readonly IUserTulRepository _userTulRepository;

        public UserTulService(IUserTulRepository userTulRepository)
        {
            _userTulRepository = userTulRepository;
        }

        public async Task<List<UserTulDto>> GetTulsAssignedToUserAsync(string? userId, string searchString)
        {
            List<UserTulDto> assignedtuls = await _userTulRepository
                .GetTulsAssignedToUserAsync(userId, searchString);

            return assignedtuls;
        }

        public async Task<UserTulDto> GetUserTul(int tulId, string userId)
        {
            return await _userTulRepository.GetUserTul(tulId, userId);
        }

        public async Task<UserTulDto> UpdateUserTulStatus(UserTulDto userTul)
        {
            return await _userTulRepository.UpdateUserTulStatus(userTul);
        }
    }
}
