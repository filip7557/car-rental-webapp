using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private string? _connectionString;

        public RoleRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<Guid> GetDefaultRoleIdAsync()
        {
            try
            {
                Role? role = null;
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT * FROM \"Role\" WHERE \"Name\" = @roleName";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("roleName", "User");

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        role = new Role()
                        {
                            Id = Guid.Parse(reader[0].ToString()!),
                            Name = reader[1].ToString()!
                        };
                    }

                    connection.Close();

                    return role!.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return Guid.Empty;
            }
        }

        //GET ALL
        public async Task<List<Role>?> GetAllAsync()
        {
            var roles = new List<Role>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\"" +
                                      "FROM \"Role\"";

                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var role = new Role()
                            {
                                Id = Guid.Parse(reader[0].ToString()!),
                                Name = reader[1].ToString()!,
                            };

                            roles.Add(role);
                        }

                        return roles;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetRoleNameByIdAsync(Guid roleId)
        {
            try
            {
                Role? role = null;
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT * FROM \"Role\" WHERE \"Id\" = @id";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, roleId);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        role = new Role()
                        {
                            Id = Guid.Parse(reader[0].ToString()!),
                            Name = reader[1].ToString()!
                        };
                    }

                    connection.Close();

                    return role!.Name;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return "";
            }
        }

        //GET BY ID
        public async Task<Role?> GetByIdAsync(Guid id)
        {
            try
            {
                Role? role = null;

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT " +
                                      "\"Role\".\"Id\", \"Name\"" +
                                      "FROM \"Role\" " +
                                      "WHERE \"Role\".\"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        role.Id = Guid.Parse(reader[0].ToString()!);
                        role.Name = reader[1].ToString()!;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return role;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Role?> GetRoleByNameAsync(string name)
        {
            try
            {
                Role? role = null;

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT" +
                                      " \"Id\", \"Name\"" +
                                      " FROM \"Role\"" +
                                      " WHERE \"Role\".\"Name\" = @name;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("name", name);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        role.Id = Guid.Parse(reader[0].ToString()!);
                        role.Name = reader[1].ToString()!;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return role;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}