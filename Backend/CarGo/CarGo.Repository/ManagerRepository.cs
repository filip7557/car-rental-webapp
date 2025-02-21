using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private string? _connectionString;

        public ManagerRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<List<User>> GetAllCompanyManagersAsync(Guid companyId)
        {
            var managers = new List<User>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        "SELECT \"User\".\"Id\", \"FullName\", \"Email\", \"RoleId\" FROM \"User\" INNER JOIN \"UserCompany\" ON \"User\".\"Id\" = \"UserCompany\".\"UserId\" WHERE \"UserCompany\".\"CompanyId\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", companyId);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                Id = Guid.Parse(reader[0].ToString()!),
                                FullName = reader[1].ToString()!,
                                Email = reader[2].ToString()!,
                                RoleId = Guid.Parse(reader[3].ToString()!)
                            };
                            managers.Add(user);
                        }
                    }

                    connection.Close();

                    return managers;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return managers;
            }
        }

        public async Task<User?> GetManagerByIdAsync(Guid userId)
        {
            User? user = null;
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        "SELECT \"Id\", \"FullName\", \"Email\", \"RoleId\" FROM \"User\" INNER JOIN \"UserCompany\" ON \"User\".\"Id\" = \"UserCompany\".\"UserId\" WHERE \"UserCompany\".\"UserId\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", userId);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        user = new User
                        {
                            Id = Guid.Parse(reader[0].ToString()!),
                            FullName = reader[1].ToString()!,
                            Email = reader[2].ToString()!,
                            RoleId = Guid.Parse(reader[3].ToString()!)
                        };
                    }

                    connection.Close();

                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return user;
            }
        }

        public async Task<bool> AddManagerToCompanyAsync(Guid companyId, User managerId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        "INSERT INTO \"UserCompany\" (\"Id\", \"UserId\", \"CompanyId\") VALUES (@id, @userId, @companyId);";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", Guid.NewGuid());
                    command.Parameters.AddWithValue("userId", managerId.Id);
                    command.Parameters.AddWithValue("companyId", companyId);
                    
                    connection.Open();

                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        connection.Close();
                        return false;
                    }

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> RemoveManagerFromCompanyAsync(Guid companyId, Guid managerId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        "DELETE FROM \"UserCompany\" WHERE \"UserId\" = @userId AND \"CompanyId\" = @companyId";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("userId", managerId);
                    command.Parameters.AddWithValue("companyId", companyId);

                    connection.Open();

                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        connection.Close();
                        return false;
                    }

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

        public async Task<Guid> GetCompanyIdByUserIdAsync(Guid userId)
        {
            Guid companyId = Guid.Empty;
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        "SELECT \"CompanyId\" FROM \"UserCompany\" WHERE \"UserId\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", userId);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        companyId = Guid.Parse(reader[0].ToString()!);
                    }

                    connection.Close();

                    return companyId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return companyId;
            }
        }
    }
}