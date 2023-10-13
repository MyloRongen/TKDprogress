using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Models;
using TKDprogress_DAL.Data;

namespace TKDprogress_DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<Category>> GetCategoriesAsync(string searchString)
        {
            List<Category> categories = new();

            try
            {
                using MySqlConnection connection = new(_connectionString);
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
                    Category category = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                    };

                    categories.Add(category);
                }
            }
            catch
            {
                categories = new()
                {
                    new Category { ErrorMessage = "De categories could not be loaded." }
                };
            }

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            Category? category = new();

            try
            {
                using MySqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                string query = "SELECT Id, Name, Description FROM Categories WHERE Id = @Id";

                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    category = new Category
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                    };
                }
            }
            catch
            {
                category.ErrorMessage = "An error occurred while trying to get the category.";
            }

            return category;
        }


        public async Task<Category> CreateCategoryAsync(Category category)
        {
            try
            {
                string query = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";
                await ExecuteNonQueryAsync(query, command =>
                {
                    command.Parameters.AddWithValue("@Name", category.Name);
                    command.Parameters.AddWithValue("@Description", category.Description);
                });
            }
            catch 
            {
                category.ErrorMessage = "An error occurred while creating the category.";
            }

            return category;
        }

        public async Task<Category> DeleteCategoryAsync(Category category)
        {
            try
            {
                string query = "DELETE FROM Categories WHERE Id = @Id";
                await ExecuteNonQueryAsync(query, command => command.Parameters.AddWithValue("@Id", category.Id));
            }
            catch
            {
                category.ErrorMessage = "An error occurred while deleting the category.";
            }

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category newCategory)
        {
            try
            {
                string query = "UPDATE Categories SET Name = @Name, Description = @Description WHERE Id = @Id";
                await ExecuteNonQueryAsync(query, command =>
                {
                    command.Parameters.AddWithValue("@Id", newCategory.Id);
                    command.Parameters.AddWithValue("@Name", newCategory.Name);
                    command.Parameters.AddWithValue("@Description", newCategory.Description);
                });
            }
            catch
            {
                newCategory.ErrorMessage = "An error occurred while updating the category.";
            }

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
