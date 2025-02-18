using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace Repository
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private string? _connectionString;

        public VehicleTypeRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        //GET ALL
        public async Task<List<VehicleType>> GetAllAsync()
        {
            var vehicleTypes = new List<VehicleType>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\"" +
                                      "FROM \"VehicleType\"";

                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var vehicleType = new VehicleType()
                            {
                                ID = Guid.Parse(reader[0].ToString()!),
                                Name = reader[1].ToString()!,
                            };

                            vehicleTypes.Add(vehicleType);
                        }
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return vehicleTypes;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //GET BY ID
        public async Task<VehicleType?> GetByIdAsync(Guid id)
        {
            try
            {
                var vehicleType = new VehicleType() { };

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT " +
                                      "\"VehicleType\".\"Id\", \"Name\"" +
                                      "FROM \"VehicleType\" " +
                                      "WHERE \"VehicleType\".\"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        vehicleType.ID = Guid.Parse(reader[0].ToString()!);
                        vehicleType.Name = reader[1].ToString()!;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return vehicleType;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}