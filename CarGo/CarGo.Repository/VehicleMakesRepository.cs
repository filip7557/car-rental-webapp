using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace Repository
{
    public class VehicleMakesRepository : IVehicleMakesRepository
    {
        private readonly string connectionString = "";

//GET ALL
        public async Task<List<VehicleMakes>> GetAllAsync()
        {
            var vehicleMakes = new List<VehicleMakes>();

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
                            var vehicleMake = new VehicleMakes()
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
        public async Task<VehicleMakes?> GetByIdAsync(Guid id)
        {
            try
            {
                var vehicleMake = new VehicleMakes() { };

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