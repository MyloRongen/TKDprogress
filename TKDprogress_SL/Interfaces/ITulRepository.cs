using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_SL.Interfaces
{
    public interface ITulRepository
    {
        Task<List<TulDto>> GetTulsAsync(string searchString);
        Task<TulDto> CreateTulAsync(TulDto newTul);
        Task<TulDto> UpdateTulAsync(TulDto newTul);
        Task<TulDto> DeleteTulAsync(TulDto tul);
    }
}
