using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress_BLL.Services
{
    public class TulMovementService : ITulMovementService
    {
        private readonly ITulMovementRepository _tulMovementRepository;

        public TulMovementService(ITulMovementRepository tulMovementRepository)
        {
            _tulMovementRepository = tulMovementRepository;
        }

        public async Task AttachMovementsToTulAsync(Tul tul, List<TulMovement> tulMovements)
        {
            if (tulMovements != null)
            {
                foreach (TulMovement tulMovement in tulMovements)
                {
                    tulMovement.TulId = tul.Id;
                }
            }

            await _tulMovementRepository.AttachMovementsToTulAsync(tulMovements);
        }

        public async Task<Tul> GetTulWithMovementByIdAsync(int tulId)
        {
            if (tulId <= 0)
            {
                return new Tul { ErrorMessage = "Invalid tul." };
            }

            Tul tul = await _tulMovementRepository.GetTulWithMovementByIdAsync(tulId);

            if (tul.Id <= 0)
            {
                return new Tul { ErrorMessage = "Tul not found." };
            }

            return tul;
        }

        public async Task DeleteTulMovementsAsync(int tulId)
        {
            await _tulMovementRepository.DeleteTulMovementsAsync(tulId);
        }
    }
}
