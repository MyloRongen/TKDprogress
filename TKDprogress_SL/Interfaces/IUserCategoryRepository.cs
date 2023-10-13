using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_SL.Interfaces
{
    public interface IUserCategoryRepository
    {
        Task<List<UserCategoryDto>> GetCategoriesAssignedToUserAsync(string? userId, string searchString);
        Task<UserCategoryDto> GetUserCategory(int categoryId, string userId);
        Task<UserCategoryDto> UpdateUserCategoryStatus(UserCategoryDto userCategory);
    }
}
