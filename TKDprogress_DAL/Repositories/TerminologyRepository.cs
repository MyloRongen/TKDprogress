﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces.Repositories;

namespace TKDprogress_DAL.Repositories
{
    public class TerminologyRepository : ITerminologyRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<Terminology>> GetTerminologiesAsync(string searchString)
        {
            List<Terminology> terminologies = new();

            try
            {
                using MySqlConnection connection = new(_connectionString);
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
                    Terminology terminology = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Word = reader.GetString("Word"),
                        Meaning = reader.GetString("Meaning"),
                        CategoryId = reader.GetInt32("CategoryId"),
                        Category = new Category
                        {
                            Id = reader.GetInt32("CategoryId"),
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description")
                        }
                    };

                    terminologies.Add(terminology);
                }
            }
            catch
            {
                terminologies = new()
                {
                    new Terminology { ErrorMessage = "De terminologies could not be loaded." }
                };
            }

            return terminologies;
        }

        public async Task<Terminology> GetTerminologyByIdAsync(int id)
        {
            Terminology? terminology = new();

            try
            {

                using MySqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                string query = "SELECT t.*, ca.* FROM Terminologies t INNER JOIN Categories ca ON t.CategoryId = ca.Id WHERE t.Id = @Id";

                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    terminology = new Terminology
                    {
                        Id = reader.GetInt32("Id"),
                        Word = reader.GetString("Word"),
                        Meaning = reader.GetString("Meaning"),
                        CategoryId = reader.GetInt32("CategoryId"),
                        Category = new Category
                        {
                            Id = reader.GetInt32("CategoryId"),
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description")
                        }
                    };
                }
            }
            catch
            {
                terminology.ErrorMessage = "An error occurred while trying to get the terminology.";
            }

            return terminology;
        }


        public async Task<Terminology> CreateTerminologyAsync(Terminology terminology)
        {
            try
            {
                string query = "INSERT INTO Terminologies (Word, Meaning, CategoryId) VALUES (@Word, @Meaning, @CategoryId)";
                await ExecuteNonQueryAsync(query, command =>
                {
                    command.Parameters.AddWithValue("@Word", terminology.Word);
                    command.Parameters.AddWithValue("@Meaning", terminology.Meaning);
                    command.Parameters.AddWithValue("@CategoryId", terminology.CategoryId);
                });
            }
            catch
            {
                terminology.ErrorMessage = "An error occurred while creating the terminology.";
            }

            return terminology;
        }

        public async Task<Terminology> DeleteTerminologyAsync(Terminology terminology)
        {
            try
            {
                string query = "DELETE FROM Terminologies WHERE Id = @Id";
                await ExecuteNonQueryAsync(query, command => command.Parameters.AddWithValue("@Id", terminology.Id));
            }
            catch
            {
                terminology.ErrorMessage = "An error occurred while deleting the terminology.";
            }

            return terminology;
        }

        public async Task<Terminology> UpdateTerminologyAsync(Terminology newTerminology)
        {
            try
            {
                string query = "UPDATE Terminologies SET Word = @Word, Meaning = @Meaning, CategoryId = @CategoryId WHERE Id = @Id";
                await ExecuteNonQueryAsync(query, command =>
                {
                    command.Parameters.AddWithValue("@Id", newTerminology.Id);
                    command.Parameters.AddWithValue("@Word", newTerminology.Word);
                    command.Parameters.AddWithValue("@Meaning", newTerminology.Meaning);
                    command.Parameters.AddWithValue("@CategoryId", newTerminology.CategoryId);
                });
            }
            catch
            {
                newTerminology.ErrorMessage = "An error occurred while updating the terminology.";
            }

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
