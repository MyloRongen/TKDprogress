using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Models;

namespace TKDprogress_UnitTest.DummyRepositories
{
    public class MovementTestRepository : IMovementRepository
    {
        private List<Movement> movements;

        public void InitializeMovements(List<Movement> newMovement)
        {
            movements = newMovement;
        }

        public async Task<List<Movement>> GetMovementsAsync(string searchString)
        {
            return movements;
        }

        public async Task<Movement> GetMovementByIdAsync(int id)
        {
            Movement movement = new();

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

        public async Task<Movement> CreateMovementAsync(Movement movement)
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

        public async Task<Movement> DeleteMovementAsync(Movement movement)
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

        public async Task<Movement> UpdateMovementAsync(Movement newMovement)
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
