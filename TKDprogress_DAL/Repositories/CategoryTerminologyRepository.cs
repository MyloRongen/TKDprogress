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
    public class CategoryTerminologyRepository : ICategoryTerminologyRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<Category> GetTerminologiesAssignedToCategoryAsync(int categoryId)
        {
            Category? categoryWithTerminologies = null;

            using MySqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT c.Id AS CategoryId, c.Name AS CategoryName, c.Description AS CategoryDescription, " +
                           "t.Id AS TerminologyId, t.Word, t.Meaning " +
                           "FROM Categories c " +
                           "INNER JOIN Terminologies t ON c.Id = t.CategoryId " +
                           "WHERE c.Id = @CategoryId";

            using MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@CategoryId", categoryId);

            using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                if (categoryWithTerminologies == null)
                {
                    categoryWithTerminologies = new Category
                    {
                        Id = reader.GetInt32("CategoryId"),
                        Name = reader.GetString("CategoryName"),
                        Description = reader.GetString("CategoryDescription"),
                        Terminologies = new List<Terminology>()
                    };
                }

                categoryWithTerminologies.Terminologies.Add(new Terminology
                {
                    Id = reader.GetInt32("TerminologyId"),
                    Word = reader.GetString("Word"),
                    Meaning = reader.GetString("Meaning"),
                    CategoryId = categoryWithTerminologies.Id,
                    Category = categoryWithTerminologies
                });
            }

            return categoryWithTerminologies;
        }
    }
}
