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
    public class TerminologyRepository : ITerminologyRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<TerminologyDto>> GetTerminologiesAsync(string searchString)
        {
            List<TerminologyDto> terminologies = new();

            using (MySqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT t.*, ca.* FROM Terminologies t INNER JOIN Categories ca ON t.CategoryId = ca.Id";

                if (!string.IsNullOrEmpty(searchString))
                {
                    query += $" WHERE Word LIKE '%{searchString}%'";
                }

                using MySqlCommand command = new(query, connection);
                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    TerminologyDto terminology = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Word = reader.GetString("Word"),
                        Meaning = reader.GetString("Meaning"),
                        CategoryId = reader.GetInt32("CategoryId"),
                        Category = new CategoryDto
                        {
                            Id = reader.GetInt32("CategoryId"),
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description")
                        }
                    };

                    terminologies.Add(terminology);
                }
            }

            return terminologies;
        }

        public async Task<TerminologyDto> GetTerminologyByIdAsync(int id)
        {
            TerminologyDto? terminology = new();

            using (MySqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT t.*, ca.* FROM Terminologies t INNER JOIN Categories ca ON t.CategoryId = ca.Id WHERE t.Id = @Id";

                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    terminology = new TerminologyDto
                    {
                        Id = reader.GetInt32("Id"),
                        Word = reader.GetString("Word"),
                        Meaning = reader.GetString("Meaning"),
                        CategoryId = reader.GetInt32("CategoryId"),
                        Category = new CategoryDto
                        {
                            Id = reader.GetInt32("CategoryId"),
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description")
                        }
                    };
                }
            }

            return terminology;
        }


        public async Task<TerminologyDto> CreateTerminologyAsync(TerminologyDto terminology)
        {
            string query = "INSERT INTO Terminologies (Word, Meaning, CategoryId) VALUES (@Word, @Meaning, @CategoryId)";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Word", terminology.Word);
                command.Parameters.AddWithValue("@Meaning", terminology.Meaning);
                command.Parameters.AddWithValue("@CategoryId", terminology.CategoryId);
            });

            return terminology;
        }

        public async Task<TerminologyDto> DeleteTerminologyAsync(TerminologyDto terminology)
        {
            string query = "DELETE FROM Terminologies WHERE Id = @Id";
            await ExecuteNonQueryAsync(query, command => command.Parameters.AddWithValue("@Id", terminology.Id));

            return terminology;
        }

        public async Task<TerminologyDto> UpdateTerminologyAsync(TerminologyDto newTerminology)
        {
            string query = "UPDATE Terminologies SET Word = @Word, Meaning = @Meaning, CategoryId = @CategoryId WHERE Id = @Id";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Id", newTerminology.Id);
                command.Parameters.AddWithValue("@Word", newTerminology.Word);
                command.Parameters.AddWithValue("@Meaning", newTerminology.Meaning);
                command.Parameters.AddWithValue("@CategoryId", newTerminology.CategoryId);
            });

            return newTerminology;
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
