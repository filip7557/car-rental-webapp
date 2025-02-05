using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly string connectionString = "ConnectionStrings__PostgresDb";

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

        public async Task AddAsync(VehicleModel vehicleModel, Guid userId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "INSERT INTO \"VehicleModel\" (\"Id\", \"MakeID\", \"TypeID\", \"Name\", \"EnginePower\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                                      "VALUES (@id, @makeID, @typeID, @name, @enginePower, @createdByUserId, @updatedByUserId);";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, vehicleModel.ID);
                    command.Parameters.AddWithValue("makeID", NpgsqlTypes.NpgsqlDbType.Integer, vehicleModel.MakeID);
                    command.Parameters.AddWithValue("typeID", NpgsqlTypes.NpgsqlDbType.Integer, vehicleModel.TypeID);
                    command.Parameters.AddWithValue("name", NpgsqlTypes.NpgsqlDbType.Text, vehicleModel.Name);
                    command.Parameters.AddWithValue("enginePower", NpgsqlTypes.NpgsqlDbType.Integer, vehicleModel.EnginePower);
                    command.Parameters.AddWithValue("createdByUserId", userId);
                    command.Parameters.AddWithValue("updatedByUserId", userId);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding vehicle model", ex);
            }
        }
        public async Task DeleteAsync(Guid id) {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "DELETE FROM \"VehicleModel\" WHERE \"Id\" = @id";
                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                await command.ExecuteNonQueryAsync();

                
            }catch(Exception ex)
            {
                throw new Exception("Error while deleting vehicle model", ex);
            }
        }
    }
}