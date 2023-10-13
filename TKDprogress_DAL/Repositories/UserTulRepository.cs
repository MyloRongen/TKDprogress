using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Enums;
using TKDprogress_BLL.Interfaces.Repositories;

namespace TKDprogress_DAL.Repositories
{
    public class UserTulRepository : IUserTulRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<UserTul>> GetTulsAssignedToUserAsync(string? userId, string searchString)
        {
            List<UserTul> userTuls = new();

            using MySqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT t.Id AS TulId, t.Name AS TulName, t.Description AS TulDescription, ut.Status, ut.UserId " +
                           "FROM Tuls t " +
                           "INNER JOIN UserTuls ut ON t.Id = ut.TulId " +
                           "WHERE ut.UserId = @UserId";

            if (!string.IsNullOrEmpty(searchString))
            {
                query += " AND t.Name LIKE @SearchString";
            }

            using MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@SearchString", "%" + searchString + "%");

            using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                UserTul userCategory = new()
                {
                    TulId = reader.GetInt32("TulId"),
                    Tul = new Tul
                    {
                        Id = reader.GetInt32("TulId"),
                        Name = reader.GetString("TulName"),
                        Description = reader.GetString("TulDescription")
                    },
                    Status = (EnumStatus)reader.GetInt32("Status"),
                    UserId = reader.GetString("UserId")
                };

                userTuls.Add(userCategory);
            }

            return userTuls;
        }

        public async Task<UserTul> GetUserTul(int tulId, string userId)
        {
            UserTul? userTul = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT ut.TulId, ut.Status FROM UserTuls ut WHERE ut.TulId = @TulId AND ut.UserId = @UserId";

                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@TulId", tulId);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    userTul = new UserTul
                    {
                        UserId = userId,
                        TulId = tulId,
                        Status = (EnumStatus)reader.GetInt32("Status"),
                    };
                }
            }

            return userTul;
        }

        public async Task<UserTul> UpdateUserTulStatus(UserTul userTul)
        {
            string query = "UPDATE UserTuls SET Status = @Status WHERE UserId = @UserId AND TulId = @TulId";
            await ExecuteNonQueryAsync(query, command =>
            {
                command.Parameters.AddWithValue("@Status", (int)userTul.Status);
                command.Parameters.AddWithValue("@UserId", userTul.UserId);
                command.Parameters.AddWithValue("@TulId", userTul.TulId);
            });

            return userTul;
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
