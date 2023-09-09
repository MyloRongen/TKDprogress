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
    public class CategoryTerminologyService : ICategoryTerminologyService
    {
        private readonly ICategoryTerminologyRepository _categoryTerminologyRepository;

        public CategoryTerminologyService(ICategoryTerminologyRepository categoryTerminologyRepository)
        {
            _categoryTerminologyRepository = categoryTerminologyRepository;
        }

        public async Task<CategoryDto> GetTerminologiesAssignedToCategoryAsync(int categoryId)
        {
            CategoryDto categoryWithTerminologies = await _categoryTerminologyRepository
                .GetTerminologiesAssignedToCategoryAsync(categoryId);

            return categoryWithTerminologies;
        }
    }
}
