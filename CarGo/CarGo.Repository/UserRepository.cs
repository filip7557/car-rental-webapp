using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class UserRepository : IUserRepository
    {
        private string? _connectionString;

        public UserRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
            ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "INSERT INTO \"User\"" +
                        " (\"Id\", \"FullName\", \"Email\", \"Password\", \"PhoneNumber\", \"RoleId\")" +
                        " VALUES" +
                        " (@id, @fullName, @email, @password, @phoneNumber, @roleId)";

                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", user.Id);
                    command.Parameters.AddWithValue("fullName", user.FullName);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("password", user.Password!);
                    command.Parameters.AddWithValue("phoneNumber", user.PhoneNumber!);
                    command.Parameters.AddWithValue("roleId", user.RoleId!);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();

                    if (affectedRows == 0)
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
                Console.WriteLine("Exception: " + ex.ToString());
                return false;
            };
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            try
            {
                User? user = null;
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT * FROM \"User\" WHERE \"Id\" = @id";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        user = new User()
                        {
                            Id = Guid.Parse(reader[0].ToString()!),
                            FullName = reader[1].ToString()!,
                            Email = reader[2].ToString()!,
                            Password = reader[3].ToString()!,
                            PhoneNumber = reader[4].ToString()!,
                            RoleId = Guid.Parse(reader[5].ToString()!)
                        };
                    }

                    connection.Close();

                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return null;
            }
        }

        public async Task<bool> UpdateUserByIdAsync(Guid id, UserDTO user)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = $"UPDATE \"User\" set \"FullName\" = @fullName, {(!string.IsNullOrEmpty(user.Email) ? "\"Email\" = @email, " : "")} {(!string.IsNullOrEmpty(user.Password) ? "\"Password\" = @password, " : "")}\"PhoneNumber\" = @phoneNumber, \"DateUpdated\" = @datetime WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("fullName", user.FullName);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("password", user.Password ?? "");
                    command.Parameters.AddWithValue("phoneNumber", user.PhoneNumber!);
                    command.Parameters.AddWithValue("datetime", DateTime.Now);
                    command.Parameters.AddWithValue("id", id);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
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
                Console.WriteLine("Exception: " + ex.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateUserRoleByUserIdAsync(Guid id, User user)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = $"UPDATE \"User\" set \"RoleId\" = @roleId, \"DateUpdated\" = @datetime WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("roleId", user.RoleId!);
                    command.Parameters.AddWithValue("datetime", DateTime.Now);
                    command.Parameters.AddWithValue("id", id);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
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
                Console.WriteLine("Exception: " + ex.ToString());
                return false;
            }
        }

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                User? user = null;
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT * FROM \"User\" WHERE \"Email\" = @email AND \"Password\" = @password";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("email", email);
                    command.Parameters.AddWithValue("password", password);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        user = new User()
                        {
                            Id = Guid.Parse(reader[0].ToString()!),
                            FullName = reader[1].ToString()!,
                            Email = reader[2].ToString()!,
                            Password = reader[3].ToString()!,
                            PhoneNumber = reader[4].ToString()!,
                            RoleId = Guid.Parse(reader[5].ToString()!)
                        };
                    }

                    connection.Close();

                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return null;
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                User? user = null;
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT * FROM \"User\" WHERE \"Email\" = @email";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("email", email);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        user = new User()
                        {
                            Id = Guid.Parse(reader[0].ToString()!),
                            FullName = reader[1].ToString()!,
                            Email = reader[2].ToString()!,
                            Password = reader[3].ToString()!,
                            PhoneNumber = reader[4].ToString()!,
                            RoleId = Guid.Parse(reader[5].ToString()!)
                        };
                    }

                    connection.Close();

                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return null;
            }
        }
    }
}