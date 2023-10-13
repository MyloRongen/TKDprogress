using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface IUserTulService
    {
        Task<List<UserTul>> GetTulsAssignedToUserAsync(string? userId, string searchString);
        Task<UserTul> GetUserTul(int tulId, string userId);
        Task<UserTul> UpdateUserTulStatus(UserTul userTul);
    }
}
