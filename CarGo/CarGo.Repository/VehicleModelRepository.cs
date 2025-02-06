using System;
using CarGo.Model;
using CarGo.Repository.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly string connectionString ;
        public VehicleModelRepository(IConfiguration config)
        {
            connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb");
        }
        
        //GET ALL
        public async Task<List<VehicleModel>> GetAllAsync()
        {
            var vehicleModels = new List<VehicleModel>();

            try
            {
                string commandText = "SELECT \"Id\", \"MakeId\", \"TypeId\", \"Name\", \"EnginePower\" FROM \"VehicleModel\"";

                using (var connection = new NpgsqlConnection(connectionString))
//Console.Write(commandText);
                using (var command = new NpgsqlCommand(commandText, connection))

                    {
                        connection.Open();

                        var reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var vehicleModel = new VehicleModel()
                                {
                                    Id = Guid.Parse(reader[0].ToString()!),
                                    MakeId = Guid.Parse(reader[1].ToString()),
                                    TypeId = Guid.Parse(reader[2].ToString()),
                                    Name = reader[3].ToString()!,
                                    EnginePower = int.TryParse(reader[4].ToString(), out int EnginePower) ? EnginePower : 0,
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                                      "\"Id\", \"MakeId\", \"TypeId\", \"Name\", \"EnginePower\" " +
                                      "FROM \"VehicleModel\" " +
                                      "WHERE \"VehicleModel\".\"Id\" = @id;";
                    Console.Write(commandText); 

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        vehicleModel.Id = Guid.Parse(reader[0].ToString()!);
                        vehicleModel.MakeId = Guid.Parse(reader[1].ToString());
                        vehicleModel.TypeId = Guid.Parse(reader[2].ToString());
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddAsync(VehicleModel vehicleModel, Guid userId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "INSERT INTO \"VehicleModel\" (\"Id\", \"MakeId\", \"TypeId\", \"Name\", \"EnginePower\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                                      "VALUES (@id, @makeID, @typeID, @name, @enginePower, @createdByUserId, @updatedByUserId);";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", Guid.NewGuid());
                    command.Parameters.AddWithValue("makeID", vehicleModel.MakeId);
                    command.Parameters.AddWithValue("typeID", vehicleModel.TypeId);
                    command.Parameters.AddWithValue("name",  vehicleModel.Name);
                    command.Parameters.AddWithValue("enginePower",  vehicleModel.EnginePower);
                    command.Parameters.AddWithValue("createdByUserId", userId);
                    command.Parameters.AddWithValue("updatedByUserId", userId);
                    Console.WriteLine(commandText);
                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync() > 0 ;
                     
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding vehicle model", ex);
            }
        }
        public async Task<bool> DeleteAsync(Guid vehicleId, Guid id) {
            const string selectCommandText = "SELECT \"IsActive\" FROM \"VehicleModel\" WHERE \"Id\" = @id";
            const string updateCommandText = "UPDATE \"VehicleModel\" SET \"IsActive\" = @newIsActive, \"UpdatedByUserId\" = @updatedByUserId WHERE \"Id\" = @id";

            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                // Dohvati trenutni status
                using var selectCommand = new NpgsqlCommand(selectCommandText, connection);
                selectCommand.Parameters.AddWithValue("id", vehicleId);
                var currentStatus = await selectCommand.ExecuteScalarAsync();

                if (currentStatus == null)
                {
                    throw new Exception("Vehicle Model not found");
                }

                bool newIsActive = !(bool)currentStatus;

                // A�uriraj status
                using var updateCommand = new NpgsqlCommand(updateCommandText, connection);
                updateCommand.Parameters.AddWithValue("id", vehicleId);
                updateCommand.Parameters.AddWithValue("newIsActive", newIsActive);
                updateCommand.Parameters.AddWithValue("updatedByUserId", id);

                return await updateCommand.ExecuteNonQueryAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating company vehicle status", ex);
            }
        }
    }
}