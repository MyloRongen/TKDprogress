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
    }
}
