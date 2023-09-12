using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_BLL.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;

        public MovementService(IMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }

        public async Task<List<MovementDto>> GetMovementsAsync(string searchString)
        {
            List<MovementDto> movements = await _movementRepository.GetMovementsAsync(searchString);

            return movements;
        }

        public async Task<MovementDto> GetMovementByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new MovementDto { ErrorMessage = "Invalid movement." };
            }

            MovementDto movement = await _movementRepository.GetMovementByIdAsync(id);

            if (movement.Id <= 0)
            {
                return new MovementDto { ErrorMessage = "Movement not found." };
            }

            return movement;
        }

        public async Task<MovementDto> CreateMovementAsync(MovementDto movement)
        {
            if (string.IsNullOrEmpty(movement.Name) || string.IsNullOrEmpty(movement.ImageUrl))
            {
                movement.ErrorMessage = "Movement name or image is incorrect.";
                return movement;
            }

            await _movementRepository.CreateMovementAsync(movement);

            return movement;
        }

        public async Task<MovementDto> UpdateMovementAsync(MovementDto movement)
        {
            if (movement.Id <= 0)
            {
                movement.ErrorMessage = "Movement type does not exist.";
                return movement;
            }

            if (string.IsNullOrEmpty(movement.Name) || string.IsNullOrEmpty(movement.ImageUrl))
            {
                movement.ErrorMessage = "Movement name or image is incorrect.";
                return movement;
            }

            await _movementRepository.UpdateMovementAsync(movement);

            return movement;
        }

        public async Task<MovementDto> DeleteMovementAsync(MovementDto movement)
        {
            if (movement.Id <= 0)
            {
                movement.ErrorMessage = "Movement type does not exist.";
                return movement;
            }

            await _movementRepository.DeleteMovementAsync(movement);

            return movement;
        }
    }
}
