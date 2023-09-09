using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_BLL.Interfaces
{
    public interface IMovementService
    {
        Task<List<MovementDto>> GetMovementsAsync(string searchString);
        Task<MovementDto> GetMovementByIdAsync(int id);
        Task<MovementDto> CreateMovementAsync(MovementDto movement);
        Task<MovementDto> UpdateMovementAsync(MovementDto newMovement);
        Task<MovementDto> DeleteMovementAsync(MovementDto movement);
    }
}
