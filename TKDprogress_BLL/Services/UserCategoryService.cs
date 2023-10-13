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
    public class UserCategoryService : IUserCategoryService
    {
        private readonly IUserCategoryRepository _userCategoryRepository;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository)
        {
            _userCategoryRepository = userCategoryRepository;
        }

        public async Task<List<UserCategory>> GetCategoriesAssignedToUserAsync(string? userId, string searchString)
        {
            List<UserCategory> assignedCategories = await _userCategoryRepository
                .GetCategoriesAssignedToUserAsync(userId, searchString);

            return assignedCategories;
        }

        public async Task<UserCategory> GetUserCategory(int categoryId, string userId)
        {
            return await _userCategoryRepository.GetUserCategory(categoryId, userId);
        }

        public async Task<UserCategory> UpdateUserCategoryStatus(UserCategory userCategory)
        {
            return await _userCategoryRepository.UpdateUserCategoryStatus(userCategory);
        }
    }
}
