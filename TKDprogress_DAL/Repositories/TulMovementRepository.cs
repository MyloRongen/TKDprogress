using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_DAL.Repositories
{
    public class TulMovementRepository : ITulMovementRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task AttachMovementsToTulAsync(List<TulMovementDto> tulMovements)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            foreach (var tulMovement in tulMovements)
            {
                string insertQuery = "INSERT INTO TulMovements (TulId, MovementId, `Order`) VALUES (@TulId, @MovementId, @Order)";

                using MySqlCommand command = new MySqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@TulId", tulMovement.TulId);
                command.Parameters.AddWithValue("@MovementId", tulMovement.MovementId);
                command.Parameters.AddWithValue("@Order", tulMovement.Order);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<TulDto> GetTulWithMovementByIdAsync(int tulId)
        {
            TulDto? tulWithMovement = new();

            try
            {
                using MySqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                string query = @"
                SELECT t.Id, t.Name AS TulName, t.Description, m.Id AS MovementId, m.Name AS MovementName, m.ImageUrl
                FROM Tuls AS t
                LEFT JOIN TulMovements AS tm ON t.Id = tm.TulId
                LEFT JOIN Movements AS m ON tm.MovementId = m.Id
                WHERE t.Id = @TulId
                ORDER BY tm.Order";

                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@TulId", tulId);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    if (tulWithMovement.Id == 0)
                    {
                        tulWithMovement = new TulDto
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("TulName"),
                            Description = reader.GetString("Description"),
                            Movements = new List<MovementDto>()
                        };
                    }

                    int? movementId = reader["MovementId"] as int?;
                    if (movementId.HasValue)
                    {
                        tulWithMovement.Movements.Add(new MovementDto
                        {
                            Id = movementId.Value,
                            Name = reader.GetString("MovementName"),
                            ImageUrl = reader.GetString("ImageUrl")
                        });
                    }
                }
            }
            catch
            {
                tulWithMovement = new()
                {
                    ErrorMessage = "An error occurred while trying to get the tul."
                };
            }

            return tulWithMovement;
        }

        public async Task DeleteTulMovementsAsync(int tulId)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string deleteQuery = "DELETE FROM TulMovements WHERE TulId = @TulId";
            using MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@TulId", tulId);
            await deleteCommand.ExecuteNonQueryAsync();
        }
    }
}
