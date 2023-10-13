using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Repositories
{
    public interface ITulMovementRepository
    {
        Task AttachMovementsToTulAsync(List<TulMovement>? tulMovements);
        Task<Tul> GetTulWithMovementByIdAsync(int tulId);
        Task DeleteTulMovementsAsync(int tulId);
    }
}
