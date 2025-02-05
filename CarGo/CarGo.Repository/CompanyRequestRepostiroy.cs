using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class CompanyRequestRepostiroy : ICompanyRequestRepository
    {
        public const string tableName = "\"Company\"";

        private string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb");

        public async Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest)
        {
            try
            {
                string commandText = "INSERT INTO \"CompanyRequest\" (\"UserId\", \"Name\", \"Email\") VALUES (@userid, @name, @email);";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("userid", NpgsqlTypes.NpgsqlDbType.Uuid, newCompanyRequest.UserId);
                        command.Parameters.AddWithValue("name", newCompanyRequest.Name);
                        command.Parameters.AddWithValue("email", newCompanyRequest.Email);

                        connection.Open();

                        var affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateCompanyRequestAsync(CompanyRequest companyRequest)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE \"CompanyRequest\" SET \"IsActive\" = @isActive, \"IsApproved\" = @isApproved WHERE \"UserId\" = @userId;";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("isActive", companyRequest.IsActive);
                    command.Parameters.AddWithValue("isApproved", companyRequest.IsApproved);
                    command.Parameters.AddWithValue("userId", NpgsqlTypes.NpgsqlDbType.Uuid, companyRequest.UserId);

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }

        public async Task<CompanyRequest?> GetCompanyRequestByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT \"UserId\", \"Name\", \"Email\", \"IsActive\", \"IsApproved\" FROM \"CompanyRequest\" WHERE \"UserId\" = @userId;";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("userId", NpgsqlTypes.NpgsqlDbType.Uuid, id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new CompanyRequest
                            {
                                UserId = reader.GetGuid(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                IsActive = reader.GetBoolean(3),
                                IsApproved = reader.GetBoolean(4)
                            };
                        }
                        return null;
                    }
                }
            }
        }
    }
}