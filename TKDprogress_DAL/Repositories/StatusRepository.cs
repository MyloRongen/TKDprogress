using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_DAL.Entities;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Enums;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_DAL.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=tkd;Uid=root;Pwd=;";

        public async Task<List<UserTulDto>> GetAllTulStatuses(string userId)
        {
            List<UserTulDto> userTulStatuses = new();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT UserId, TulId, Status FROM UserTuls WHERE UserId = @UserId";

                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    UserTulDto userTulStatus = new()
                    {
                        UserId = reader.GetString("UserId"),
                        TulId = reader.GetInt32("TulId"),
                        Status = (EnumStatus)reader.GetInt32("Status"),
                    };

                    userTulStatuses.Add(userTulStatus);
                }
            }

            return userTulStatuses;
        }

        public async Task<List<UserCategoryDto>> GetAllCategoryStatuses(string userId)
        {
            List<UserCategoryDto> userTulStatuses = new();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT UserId, CategoryId, Status FROM UserCategories WHERE UserId = @UserId";

                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    UserCategoryDto userCategoryStatus = new()
                    {
                        UserId = reader.GetString("UserId"),
                        CategoryId = reader.GetInt32("CategoryId"),
                        Status = (EnumStatus)reader.GetInt32("Status"),
                    };

                    userTulStatuses.Add(userCategoryStatus);
                }
            }

            return userTulStatuses;
        }
    }
}
