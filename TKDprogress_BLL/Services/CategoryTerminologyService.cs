using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress_BLL.Services
{
    public class CategoryTerminologyService : ICategoryTerminologyService
    {
        private readonly ICategoryTerminologyRepository _categoryTerminologyRepository;

        public CategoryTerminologyService(ICategoryTerminologyRepository categoryTerminologyRepository)
        {
            _categoryTerminologyRepository = categoryTerminologyRepository;
        }

        public async Task<Category> GetTerminologiesAssignedToCategoryAsync(int categoryId)
        {
            Category categoryWithTerminologies = await _categoryTerminologyRepository
                .GetTerminologiesAssignedToCategoryAsync(categoryId);

            return categoryWithTerminologies;
        }
    }
}
