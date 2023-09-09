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
            TulDto tul = await _tulMovementRepository.GetTulWithMovementByIdAsync(tulId);

            return tul;
        }

        public async Task DeleteTulMovementsAsync(int tulId)
        {
            await _tulMovementRepository.DeleteTulMovementsAsync(tulId);
        }
    }
}
