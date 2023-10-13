using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress_BLL.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;

        public MovementService(IMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }

        public async Task<List<Movement>> GetMovementsAsync(string searchString)
        {
            List<Movement> movements = await _movementRepository.GetMovementsAsync(searchString);

            return movements;
        }

        public async Task<Movement> GetMovementByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Movement { ErrorMessage = "Invalid movement." };
            }

            Movement movement = await _movementRepository.GetMovementByIdAsync(id);

            if (movement.Id <= 0)
            {
                return new Movement { ErrorMessage = "Movement not found." };
            }

            return movement;
        }

        public async Task<Movement> CreateMovementAsync(Movement movement)
        {
            if (string.IsNullOrEmpty(movement.Name) || string.IsNullOrEmpty(movement.ImageUrl))
            {
                movement.ErrorMessage = "Movement name or image is incorrect.";
                return movement;
            }

            await _movementRepository.CreateMovementAsync(movement);

            return movement;
        }

        public async Task<Movement> UpdateMovementAsync(Movement movement)
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

        public async Task<Movement> DeleteMovementAsync(Movement movement)
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
