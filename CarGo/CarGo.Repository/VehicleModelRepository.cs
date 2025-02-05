using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly string connectionString = "";

//GET ALL
        public async Task<List<VehicleModel>> GetAllAsync()
        {
            var vehicleModels = new List<VehicleModel>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT \"Id\", \"MakeID\", \"TypeID\", \"Name\", \"EnginePower\"" +
                                      "FROM \"VehicleModel\"";

                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var vehicleModel = new VehicleModel()
                            {
                                ID = Guid.Parse(reader[0].ToString()!),
                                MakeID = int.TryParse(reader[1].ToString(), out int makeID) ? makeID : 0,
                                TypeID = int.TryParse(reader[2].ToString(), out int typeID) ? typeID : 0,
                                Name = reader[3].ToString()!,
                                EnginePower = int.TryParse(reader[4].ToString(), out int enginePower) ? enginePower : 0,
                            };

                            vehicleModels.Add(vehicleModel);
                        }
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return vehicleModels;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

//GET BY ID
        public async Task<VehicleModel?> GetByIdAsync(Guid id)
        {
            try
            {
                var vehicleModel = new VehicleModel() { };

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT " +
                                      "\"ID\", \"MakeID\", \"TypeID\", \"Name\", \"EnginePower\"" +
                                      "FROM \"VehicleModel\" " +
                                      "WHERE \"VehicleModel\".\"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        vehicleModel.ID = Guid.Parse(reader[0].ToString()!);
                        vehicleModel.MakeID = int.TryParse(reader[1].ToString(), out int makeID) ? makeID : 0;
                        vehicleModel.TypeID = int.TryParse(reader[2].ToString(), out int typeID) ? typeID : 0;
                        vehicleModel.Name = reader[3].ToString()!;
                        vehicleModel.EnginePower =
                            int.TryParse(reader[4].ToString(), out int enginePower) ? enginePower : 0;
                    }

                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return vehicleModel;
                }
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}