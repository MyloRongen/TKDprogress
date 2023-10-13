using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface IMovementService
    {
        Task<List<Movement>> GetMovementsAsync(string searchString);
        Task<Movement> GetMovementByIdAsync(int id);
        Task<Movement> CreateMovementAsync(Movement movement);
        Task<Movement> UpdateMovementAsync(Movement newMovement);
        Task<Movement> DeleteMovementAsync(Movement movement);
    }
}
