using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface ITulService
    {
        Task<List<Tul>> GetTulsAsync(string searchString);
        Task<Tul> CreateTulAsync(Tul newTul);
        Task<Tul> UpdateTulAsync(Tul newTul);
        Task<Tul> DeleteTulAsync(Tul tul);
    }
}
