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
        /*private string connectionString = "Host=localhost:5432;" + "Username=postgres;" +
            "Password=12345;" +
            "Database=CarGo";
        */

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
                        command.Parameters.AddWithValue("name", NpgsqlTypes.NpgsqlDbType.Text, newCompanyRequest.Name);
                        command.Parameters.AddWithValue("email", NpgsqlTypes.NpgsqlDbType.Text, newCompanyRequest.Email);

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
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AcceptCompanyRequest(CompanyRequest acceptedCompanyRequest)
        {
            try
            {
                string commandText = "INSERT INTO \"Company\" (\"UserId\", \"Name\", \"Email\") VALUES (@userid, @name, @email);";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("userid", NpgsqlTypes.NpgsqlDbType.Uuid, acceptedCompanyRequest.UserId);
                        command.Parameters.AddWithValue("name", NpgsqlTypes.NpgsqlDbType.Text, acceptedCompanyRequest.Name);
                        command.Parameters.AddWithValue("email", NpgsqlTypes.NpgsqlDbType.Text, acceptedCompanyRequest.Email);

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
            catch (Exception)
            {
                return false;
            }
        }
    }
}