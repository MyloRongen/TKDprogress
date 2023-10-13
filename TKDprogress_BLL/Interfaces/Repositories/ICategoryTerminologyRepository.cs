using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Repositories
{
    public interface ICategoryTerminologyRepository
    {
        Task<Category> GetTerminologiesAssignedToCategoryAsync(int categoryId);
    }
}
