using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Repositories
{
    public interface IUserTulRepository
    {
        Task<List<UserTul>> GetTulsAssignedToUserAsync(string? userId, string searchString);
        Task<UserTul> GetUserTul(int tulId, string userId);
        Task<UserTul> UpdateUserTulStatus(UserTul userTul);
    }
}
