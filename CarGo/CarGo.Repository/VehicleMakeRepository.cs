using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly string connectionString = "";

//GET ALL
        public async Task<List<VehicleMake>> GetAllAsync()
        {
            var vehicleMakes = new List<VehicleMake>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\"" +
                                      "FROM \"VehicleMake\"";

                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var vehicleMake = new VehicleMake()
                            {
                                ID = Guid.Parse(reader[0].ToString()!),
                                Name = reader[1].ToString()!,
                            };

                            vehicleMakes.Add(vehicleMake);
                        }
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return vehicleMakes;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

//GET BY ID
        public async Task<VehicleMake?> GetByIdAsync(Guid id)
        {
            try
            {
                var vehicleMake = new VehicleMake() { };

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT " +
                                      "\"VehicleMake\".\"Id\", \"Name\"" +
                                      "FROM \"VehicleMake\" " +
                                      "WHERE \"VehicleMake\".\"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        vehicleMake.ID = Guid.Parse(reader[0].ToString()!);
                        vehicleMake.Name = reader[1].ToString()!;
                    }

                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return vehicleMake;
                }
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}