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
    public class UserCategoryService : IUserCategoryService
    {
        private readonly IUserCategoryRepository _userCategoryRepository;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository)
        {
            _userCategoryRepository = userCategoryRepository;
        }

        public async Task<List<UserCategoryDto>> GetCategoriesAssignedToUserAsync(string? userId, string searchString)
        {
            List<UserCategoryDto> assignedCategories = await _userCategoryRepository
                .GetCategoriesAssignedToUserAsync(userId, searchString);

            return assignedCategories;
        }

        public async Task<UserCategoryDto> GetUserCategory(int categoryId, string userId)
        {
            return await _userCategoryRepository.GetUserCategory(categoryId, userId);
        }

        public async Task<UserCategoryDto> UpdateUserCategoryStatus(UserCategoryDto userCategory)
        {
            return await _userCategoryRepository.UpdateUserCategoryStatus(userCategory);
        }
    }
}
