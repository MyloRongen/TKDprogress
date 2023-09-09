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
    public class TulRepository : ITulRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<TulDto>> GetTulsAsync(string searchString)
        {
            List<TulDto> tuls = new();

            using (MySqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, Description FROM Tuls";

                if (!string.IsNullOrEmpty(searchString))
                {
                    query += $" WHERE Name LIKE '%{searchString}%'";
                }

                using MySqlCommand command = new(query, connection);
                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    TulDto tul = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                    };

                    tuls.Add(tul);
                }
            }

            return tuls;
        }

        public async Task<TulDto> CreateTulAsync(TulDto newTul)
        {
            string insertQuery = "INSERT INTO Tuls (Name, Description) VALUES (@Name, @Description); SELECT LAST_INSERT_ID();";

            int tulId = 0;

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            using MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", newTul.Name);
            command.Parameters.AddWithValue("@Description", newTul.Description);

            using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                await reader.ReadAsync();
                tulId = Convert.ToInt32(reader[0]);
            }

            newTul.Id = tulId;

            return newTul;
        }

        public async Task<TulDto> DeleteTulAsync(TulDto tul)
        {
            string query = "DELETE FROM Tuls WHERE Id = @Id";
            await ExecuteNonQueryAsync(query, command => command.Parameters.AddWithValue("@Id", tul.Id));

            return tul;
        }

        public async Task<TulDto> UpdateTulAsync(TulDto newTul)
        {
            string query = "UPDATE Tuls SET Name = @Name, Description = @Description WHERE Id = @Id";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Id", newTul.Id);
                command.Parameters.AddWithValue("@Name", newTul.Name);
                command.Parameters.AddWithValue("@Description", newTul.Description);
            });

            return newTul;
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
