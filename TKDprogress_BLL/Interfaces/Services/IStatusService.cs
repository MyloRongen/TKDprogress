using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface IStatusService
    {
        Task<List<UserTul>> GetAllTulStatuses(string userId);
        Task<List<UserCategory>> GetAllCategoryStatuses(string userId);
    }
}
