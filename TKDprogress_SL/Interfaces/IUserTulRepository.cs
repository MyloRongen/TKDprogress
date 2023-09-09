using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_SL.Interfaces
{
    public interface IUserTulRepository
    {
        Task<List<UserTulDto>> GetTulsAssignedToUserAsync(string? userId, string searchString);
        Task<UserTulDto> GetUserTul(int tulId, string userId);
        Task<UserTulDto> UpdateUserTulStatus(UserTulDto userTul);
    }
}
