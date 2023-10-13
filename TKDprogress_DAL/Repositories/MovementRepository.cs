using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces.Repositories;

namespace TKDprogress_DAL.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<Movement>> GetMovementsAsync(string searchString)
        {
            List<Movement> movements = new();

            try
            {
                using MySqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                string query = "SELECT Id, Name, ImageUrl FROM Movements";

                if (!string.IsNullOrEmpty(searchString))
                {
                    query += $" WHERE Name LIKE '%{searchString}%'";
                }

                using MySqlCommand command = new(query, connection);
                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Movement movement = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        ImageUrl = reader.GetString("ImageUrl"),
                    };

                    movements.Add(movement);
                }
            }
            catch 
            {
                movements = new()
                {
                    new Movement { ErrorMessage = "De movements could not be loaded." }
                };
            }

            return movements;
        }

        public async Task<Movement> GetMovementByIdAsync(int id)
        {
            Movement? movement = new();

            try
            {
                using MySqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                string query = "SELECT Id, Name, ImageUrl FROM Movements WHERE Id = @Id";

                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    movement = new Movement
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        ImageUrl = reader.GetString("ImageUrl"),
                    };
                }
            }
            catch
            {
                movement.ErrorMessage = "An error occurred while trying to get the movement.";
            }

            return movement;
        }

        public async Task<Movement> CreateMovementAsync(Movement movement)
        {
            try
            {
                string query = "INSERT INTO Movements (Name, ImageUrl) VALUES (@Name, @ImageUrl)";
                await ExecuteNonQueryAsync(query, command =>
                {
                    command.Parameters.AddWithValue("@Name", movement.Name);
                    command.Parameters.AddWithValue("@ImageUrl", movement.ImageUrl);
                });
            }
            catch
            {
                movement.ErrorMessage = "An error occurred while creating the movement.";
            }

            return movement;
        }

        public async Task<Movement> DeleteMovementAsync(Movement movement)
        {
            try
            {
                string query = "DELETE FROM Movements WHERE Id = @Id";
                await ExecuteNonQueryAsync(query, command => command.Parameters.AddWithValue("@Id", movement.Id));

            }
            catch
            {
                movement.ErrorMessage = "An error occurred while deleting the movement.";
            }

            return movement;           
        }

        public async Task<Movement> UpdateMovementAsync(Movement newMovement)
        {
            try
            {
                string query = "UPDATE Movements SET Name = @Name, ImageUrl = @ImageUrl WHERE Id = @Id";
                await ExecuteNonQueryAsync(query, command =>
                {
                    command.Parameters.AddWithValue("@Id", newMovement.Id);
                    command.Parameters.AddWithValue("@Name", newMovement.Name);
                    command.Parameters.AddWithValue("@ImageUrl", newMovement.ImageUrl);
                });
            }
            catch
            {
                newMovement.ErrorMessage = "An error occurred while updating the movement.";
            }

            return newMovement;
        }

        private async Task ExecuteNonQueryAsync(string query, Action<MySqlCommand> parameterAction)
        {
            using MySqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            using MySqlCommand command = new(query, connection);
            parameterAction(command);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected <= 0)
            {
                throw new Exception("Operation failed.");
            }
        }
    }
}
