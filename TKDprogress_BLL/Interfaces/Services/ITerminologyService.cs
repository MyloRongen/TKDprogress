using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface ITerminologyService
    {
        Task<List<Terminology>> GetTerminologiesAsync(string searchString);

        Task<Terminology> GetTerminologyByIdAsync(int id);

        Task<Terminology> CreateTerminologyAsync(Terminology terminology);

        Task<Terminology> UpdateTerminologyAsync(Terminology newTerminology);

        Task<Terminology> DeleteTerminologyAsync(Terminology terminology);
    }
}
