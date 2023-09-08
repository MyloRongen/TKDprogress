using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_SL.Interfaces
{
    public interface ITerminologyRepository
    {
        Task<List<TerminologyDto>> GetTerminologiesAsync(string searchString);

        Task<TerminologyDto> GetTerminologyByIdAsync(int id);

        Task<TerminologyDto> CreateTerminologyAsync(TerminologyDto terminology);

        Task<TerminologyDto> DeleteTerminologyAsync(TerminologyDto terminology);

        Task<TerminologyDto> UpdateTerminologyAsync(TerminologyDto newTerminology);
    }
}
