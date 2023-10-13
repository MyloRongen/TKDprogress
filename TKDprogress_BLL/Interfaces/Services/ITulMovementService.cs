using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;

namespace TKDprogress_BLL.Interfaces.Services
{
    public interface ITulMovementService
    {
        Task AttachMovementsToTulAsync(Tul tul, List<TulMovement> tulMovements);
        Task<Tul> GetTulWithMovementByIdAsync(int tulId);
        Task DeleteTulMovementsAsync(int tulId);
    }
}
