using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Enums;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_DAL.Repositories
{
    public class UserCategoryRepository : IUserCategoryRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<UserCategoryDto>> GetCategoriesAssignedToUserAsync(string? userId, string searchString)
        {
            List<UserCategoryDto> userCategories = new();

            using MySqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT c.Id AS CategoryId, c.Name AS CategoryName, c.Description AS CategoryDescription, uc.Status, uc.UserId " +
                           "FROM Categories c " +
                           "INNER JOIN UserCategories uc ON c.Id = uc.CategoryId " +
                           "WHERE uc.UserId = @UserId";

            if (!string.IsNullOrEmpty(searchString))
            {
                query += " AND c.Name LIKE @SearchString";
            }

            using MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@SearchString", "%" + searchString + "%");

            using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                UserCategoryDto userCategory = new()
                {
                    CategoryId = reader.GetInt32("CategoryId"),
                    Category = new CategoryDto
                    {
                        Id = reader.GetInt32("CategoryId"),
                        Name = reader.GetString("CategoryName"),
                        Description = reader.GetString("CategoryDescription")
                    },
                    Status = (EnumStatus)reader.GetInt32("Status"),
                    UserId = reader.GetString("UserId")
                };

                userCategories.Add(userCategory);
            }

            return userCategories;
        }
    }
}
