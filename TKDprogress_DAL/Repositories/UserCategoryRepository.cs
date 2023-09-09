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

        public async Task<UserCategoryDto> GetUserCategory(int categoryId, string userId)
        {
            UserCategoryDto? userCategory = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT uc.CategoryId, uc.Status FROM UserCategories uc WHERE uc.CategoryId = @CategoryId AND uc.UserId = @UserId";

                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                command.Parameters.AddWithValue("@UserId", userId);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    userCategory = new UserCategoryDto
                    {
                        UserId = userId,
                        CategoryId = categoryId, 
                        Status = (EnumStatus)reader.GetInt32("Status"), 
                    };
                }
            }

            return userCategory;
        }

        public async Task<UserCategoryDto> UpdateUserCategoryStatus(UserCategoryDto userCategory)
        {
            string query = "UPDATE UserCategories SET Status = @Status WHERE UserId = @UserId AND CategoryId = @CategoryId";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Status", (int)userCategory.Status);
                command.Parameters.AddWithValue("@UserId", userCategory.UserId);
                command.Parameters.AddWithValue("@CategoryId", userCategory.CategoryId);
            });

            return userCategory;
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
