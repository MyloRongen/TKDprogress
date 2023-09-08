﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_BLL.Interfaces
{
    public interface IUserCategoryService
    {
        Task<List<UserCategoryDto>> GetCategoriesAssignedToUserAsync(string? userId, string searchString);
    }
}