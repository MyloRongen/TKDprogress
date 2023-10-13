using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface IUserCategoryService
    {
        Task<List<UserCategory>> GetCategoriesAssignedToUserAsync(string? userId, string searchString);
        Task<UserCategory> GetUserCategory(int categoryId, string userId);
        Task<UserCategory> UpdateUserCategoryStatus(UserCategory userCategory);
    }
}
