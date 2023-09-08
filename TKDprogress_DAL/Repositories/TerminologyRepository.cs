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

                string query = "SELECT Id, Word, Meaning FROM Terminologies";

                if (!string.IsNullOrEmpty(searchString))
                {
                    query += $" WHERE Name LIKE '%{searchString}%'";
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

                string query = "SELECT Id, Word, Meaning FROM Terminologies WHERE Id = @Id";

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
                    };
                }
            }

            return terminology;
        }


        public async Task<TerminologyDto> CreateTerminologyAsync(TerminologyDto terminology)
        {
            string query = "INSERT INTO Terminologies (Word, Meaning) VALUES (@Word, @Meaning)";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Word", terminology.Word);
                command.Parameters.AddWithValue("@Meaning", terminology.Meaning);
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
            string query = "UPDATE Terminologies SET Word = @Word, Meaning = @Meaning WHERE Id = @Id";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Id", newTerminology.Id);
                command.Parameters.AddWithValue("@Word", newTerminology.Word);
                command.Parameters.AddWithValue("@Meaning", newTerminology.Meaning);
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
