using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;

namespace TKDprogress_SL.Interfaces
{
    public interface ITulMovementRepository
    {
        Task AttachMovementsToTulAsync(List<TulMovementDto>? tulMovements);
    }
}
