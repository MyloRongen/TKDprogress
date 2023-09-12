using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_UnitTest.DummyRepositories
{
    public class MovementTestRepository : IMovementRepository
    {
        private List<MovementDto> movements;

        public void InitializeMovements(List<MovementDto> newMovement)
        {
            movements = newMovement;
        }

        public async Task<List<MovementDto>> GetMovementsAsync(string searchString)
        {
            return movements;
        }

        public async Task<MovementDto> GetMovementByIdAsync(int id)
        {
            MovementDto movement = new();

            foreach (var item in movements)
            {
                if (item.Id == id)
                {
                    movement = item;
                    break;
                }
            }

            return movement;
        }

        public async Task<MovementDto> CreateMovementAsync(MovementDto movement)
        {
            foreach (var item in movements)
            {
                if (movement.Id == item.Id)
                {
                    throw new Exception("This Movement already exists!");
                }
            }

            movements.Add(movement);
            return movement;
        }

        public async Task<MovementDto> DeleteMovementAsync(MovementDto movement)
        {
            foreach (var item in movements)
            {
                if (movement.Id == item.Id)
                {
                    movements.Remove(item);
                    break;
                }
            }

            return movement;
        }

        public async Task<MovementDto> UpdateMovementAsync(MovementDto newMovement)
        {
            for (int i = 0; i < movements.Count; i++)
            {
                if (newMovement.Id == movements[i].Id)
                {
                    movements[i] = newMovement;
                    break;
                }
            }

            return newMovement;
        }
    }
}
