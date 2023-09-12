using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_BLL.Services
{
    public class TulMovementService : ITulMovementService
    {
        private readonly ITulMovementRepository _tulMovementRepository;

        public TulMovementService(ITulMovementRepository tulMovementRepository)
        {
            _tulMovementRepository = tulMovementRepository;
        }

        public async Task AttachMovementsToTulAsync(TulDto tul, List<TulMovementDto> tulMovements)
        {
            if (tulMovements != null)
            {
                foreach (TulMovementDto tulMovement in tulMovements)
                {
                    tulMovement.TulId = tul.Id;
                }
            }

            await _tulMovementRepository.AttachMovementsToTulAsync(tulMovements);
        }

        public async Task<TulDto> GetTulWithMovementByIdAsync(int tulId)
        {
            if (tulId <= 0)
            {
                return new TulDto { ErrorMessage = "Invalid tul." };
            }

            TulDto tul = await _tulMovementRepository.GetTulWithMovementByIdAsync(tulId);

            if (tul.Id <= 0)
            {
                return new TulDto { ErrorMessage = "Tul not found." };
            }

            return tul;
        }

        public async Task DeleteTulMovementsAsync(int tulId)
        {
            await _tulMovementRepository.DeleteTulMovementsAsync(tulId);
        }
    }
}
