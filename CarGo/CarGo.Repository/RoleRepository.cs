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
    }
}