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
    public class TerminologyService : ITerminologyService
    {
        private readonly ITerminologyRepository _terminologyRepository;

        public TerminologyService(ITerminologyRepository terminologyRepository)
        {
            _terminologyRepository = terminologyRepository;
        }

        public async Task<List<Terminology>> GetTerminologiesAsync(string searchString)
        {
            List<Terminology> terminologies = await _terminologyRepository.GetTerminologiesAsync(searchString);

            return terminologies;
        }

        public async Task<Terminology> GetTerminologyByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Terminology { ErrorMessage = "Invalid terminology." };
            }

            Terminology terminology = await _terminologyRepository.GetTerminologyByIdAsync(id);

            if (terminology.Id <= 0)
            {
                return new Terminology { ErrorMessage = "Terminology not found." };
            }

            return terminology;
        }

        public async Task<Terminology> CreateTerminologyAsync(Terminology terminology)
        {
            if (terminology.CategoryId <= 0)
            {
                terminology.ErrorMessage = "Category type does not exist.";
                return terminology;
            }

            if (string.IsNullOrEmpty(terminology.Word) || string.IsNullOrEmpty(terminology.Meaning))
            {
                terminology.ErrorMessage = "Terminology name or description has an incorrect input.";
                return terminology;
            }

            await _terminologyRepository.CreateTerminologyAsync(terminology);

            return terminology;
        }

        public async Task<Terminology> UpdateTerminologyAsync(Terminology terminology)
        {
            if (terminology.Id <= 0)
            {
                terminology.ErrorMessage = "Terminology does not exist.";
                return terminology;
            }

            if (terminology.CategoryId <= 0)
            {
                terminology.ErrorMessage = "Category type does not exist.";
                return terminology;
            }

            if (string.IsNullOrEmpty(terminology.Word) || string.IsNullOrEmpty(terminology.Meaning))
            {
                terminology.ErrorMessage = "Terminology name or description has an incorrect input.";
                return terminology;
            }

            await _terminologyRepository.UpdateTerminologyAsync(terminology);

            return terminology;
        }

        public async Task<Terminology> DeleteTerminologyAsync(Terminology terminology)
        {
            if (terminology.Id <= 0)
            {
                terminology.ErrorMessage = "Terminology does not exist.";
                return terminology;
            }

            await _terminologyRepository.DeleteTerminologyAsync(terminology);

            return terminology;
        }
    }
}
