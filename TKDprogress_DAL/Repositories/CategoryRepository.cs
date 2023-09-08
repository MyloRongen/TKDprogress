using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_DAL.Data;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<CategoryDto>> GetCategoriesAsync(string searchString)
        {
            List<CategoryDto> categories = new();

            using (MySqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, Description FROM Categories";

                if (!string.IsNullOrEmpty(searchString))
                {
                    query += $" WHERE Name LIKE '%{searchString}%'";
                }

                using MySqlCommand command = new(query, connection);
                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    CategoryDto category = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                    };

                    categories.Add(category);
                }
            }

            return categories;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            CategoryDto? category = new();

            using (MySqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, Description FROM Categories WHERE Id = @Id";

                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    category = new CategoryDto
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                    };
                }
            }

            return category;
        }


        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto category)
        {
            string query = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Name", category.Name);
                command.Parameters.AddWithValue("@Description", category.Description);
            });

            return category;
        }

        public async Task<CategoryDto> DeleteCategoryAsync(CategoryDto category)
        {
            string query = "DELETE FROM Categories WHERE Id = @Id";
            await ExecuteNonQueryAsync(query, command => command.Parameters.AddWithValue("@Id", category.Id));

            return category;
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto newCategory)
        {
            string query = "UPDATE Categories SET Name = @Name, Description = @Description WHERE Id = @Id";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Id", newCategory.Id);
                command.Parameters.AddWithValue("@Name", newCategory.Name);
                command.Parameters.AddWithValue("@Description", newCategory.Description);
            });

            return newCategory;
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
