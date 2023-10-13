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
    public class TulRepository : ITulRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<Tul>> GetTulsAsync(string searchString)
        {
            List<Tul> tuls = new();

            try
            {
                using MySqlConnection connection = new(_connectionString);
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
                    Tul tul = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                    };

                    tuls.Add(tul);
                }
            }
            catch
            {
                tuls = new()
                {
                    new Tul { ErrorMessage = "De tuls could not be loaded." }
                };
            }

            return tuls;
        }

        public async Task<Tul> CreateTulAsync(Tul newTul)
        {
            try
            {
                string insertQuery = "INSERT INTO Tuls (Name, Description) VALUES (@Name, @Description); SELECT LAST_INSERT_ID();";

                using MySqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                using MySqlCommand command = new(insertQuery, connection);
                command.Parameters.AddWithValue("@Name", newTul.Name);
                command.Parameters.AddWithValue("@Description", newTul.Description);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                int tulId = 0;

                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    tulId = Convert.ToInt32(reader[0]);
                }

                newTul.Id = tulId;
            }
            catch
            {
                newTul.ErrorMessage = "An error occurred while creating the tul.";
            }

            return newTul;
        }

        public async Task<Tul> DeleteTulAsync(Tul tul)
        {
            try
            {
                string query = "DELETE FROM Tuls WHERE Id = @Id";
                await ExecuteNonQueryAsync(query, command => command.Parameters.AddWithValue("@Id", tul.Id));
            }
            catch
            {
                tul.ErrorMessage = "An error occurred while deleting the tul.";
            }

            return tul;
        }

        public async Task<Tul> UpdateTulAsync(Tul newTul)
        {
            try
            {
                string query = "UPDATE Tuls SET Name = @Name, Description = @Description WHERE Id = @Id";
                await ExecuteNonQueryAsync(query, command =>
                {
                    command.Parameters.AddWithValue("@Id", newTul.Id);
                    command.Parameters.AddWithValue("@Name", newTul.Name);
                    command.Parameters.AddWithValue("@Description", newTul.Description);
                });
            }
            catch
            {
                newTul.ErrorMessage = "An error occurred while creating the tul.";
            }

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
