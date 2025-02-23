using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace Repository
{
    public class VehicleStatusRepository : IVehicleStatusRepository
    {
        private string? _connectionString;

        public VehicleStatusRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        //GET ALL
        public async Task<List<VehicleStatus>> GetAllAsync()
        {
            var vehicleStatuses = new List<VehicleStatus>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\"" +
                                      "FROM \"VehicleStatus\"";

                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var vehicleStatus = new VehicleStatus()
                            {
                                ID = Guid.Parse(reader[0].ToString()!),
                                Name = reader[1].ToString()!,
                            };

                            vehicleStatuses.Add(vehicleStatus);
                        }
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return vehicleStatuses;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //GET BY ID
        public async Task<VehicleStatus?> GetByIdAsync(Guid id)
        {
            try
            {
                var vehicleStatus = new VehicleStatus() { };

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT " +
                                      "\"VehicleStatus\".\"Id\", \"Name\"" +
                                      "FROM \"VehicleStatus\" " +
                                      "WHERE \"VehicleStatus\".\"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        vehicleStatus.ID = Guid.Parse(reader[0].ToString()!);
                        vehicleStatus.Name = reader[1].ToString()!;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return vehicleStatus;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}