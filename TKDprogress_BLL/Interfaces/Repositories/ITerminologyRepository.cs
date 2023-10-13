using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Repositories
{
    public interface ITerminologyRepository
    {
        Task<List<Terminology>> GetTerminologiesAsync(string searchString);

        Task<Terminology> GetTerminologyByIdAsync(int id);

        Task<Terminology> CreateTerminologyAsync(Terminology terminology);

        Task<Terminology> DeleteTerminologyAsync(Terminology terminology);

        Task<Terminology> UpdateTerminologyAsync(Terminology newTerminology);
    }
}
