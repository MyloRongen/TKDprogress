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
    public class TerminologyService : ITerminologyService
    {
        private readonly ITerminologyRepository _terminologyRepository;

        public TerminologyService(ITerminologyRepository terminologyRepository)
        {
            _terminologyRepository = terminologyRepository;
        }

        public async Task<List<TerminologyDto>> GetTerminologiesAsync(string searchString)
        {
            List<TerminologyDto> terminologies = await _terminologyRepository.GetTerminologiesAsync(searchString);

            return terminologies;
        }

        public async Task<TerminologyDto> GetTerminologyByIdAsync(int id)
        {
            TerminologyDto artist = await _terminologyRepository.GetTerminologyByIdAsync(id);

            return artist;
        }

        public async Task<TerminologyDto> CreateTerminologyAsync(TerminologyDto terminology)
        {
            await _terminologyRepository.CreateTerminologyAsync(terminology);

            return terminology;
        }

        public async Task<TerminologyDto> UpdateTerminologyAsync(TerminologyDto terminology)
        {
            await _terminologyRepository.UpdateTerminologyAsync(terminology);

            return terminology;
        }

        public async Task<TerminologyDto> DeleteTerminologyAsync(TerminologyDto terminology)
        {
            await _terminologyRepository.DeleteTerminologyAsync(terminology);

            return terminology;
        }
    }
}
