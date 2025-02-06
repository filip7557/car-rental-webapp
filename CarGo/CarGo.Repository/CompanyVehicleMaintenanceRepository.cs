using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class CompanyVehicleMaintenanceRepository : ICompanyVehicleMaintenanceRepository
    {
        private string? _connectionString;

        public CompanyVehicleMaintenanceRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance,
            Guid createdByUserId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "INSERT INTO \"CompanyVehicleMaintenance\"" +
                                         " (\"Id\", \"CompanyVehicleId\", \"Title\", \"Description\", \"CreatedByUserId\", \"UpdatedByUserId\")" +
                                         " VALUES" +
                                         " (@id, @companyVehicleId, @title, @description, @createdBy, @updatedBy)";

                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", Guid.NewGuid());
                    command.Parameters.AddWithValue("companyVehicleId", maintenance.CompanyVehicleId);
                    command.Parameters.AddWithValue("title", maintenance.Title);
                    command.Parameters.AddWithValue("description", maintenance.Description ?? "");
                    command.Parameters.AddWithValue("createdBy", createdByUserId);
                    command.Parameters.AddWithValue("updatedBy", createdByUserId);

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
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCompanyVehicleMaintenanceByIdAsync(Guid maintenanceId, Guid userId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        "UPDATE \"CompanyVehicleMaintenance\" set \"IsActive\" = @value, \"UpdatedByUserId\" = @userId WHERE \"Id\" = @id";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("value", false);
                    command.Parameters.AddWithValue("userId", userId);
                    command.Parameters.AddWithValue("id", maintenanceId);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
                    {
                        connection.Close();
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

        public async Task<List<CompanyVehicleMaintenance>> GetMaintenancesByCompanyVehicleIdAsync(Guid companyVehicleId,
            Paging paging, bool isActiveFilter)
        {
            var maintenances = new List<CompanyVehicleMaintenance>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        $"SELECT \"CompanyVehicleMaintenance\".\"Id\", \"CompanyVehicleId\", \"Title\", \"Description\", \"DateCreated\" FROM \"CompanyVehicleMaintenance\" WHERE \"CompanyVehicleId\" = @companyVehId{(!isActiveFilter ? " AND \"IsActive = @isActive" : "")} ORDER BY \"DateCreated\" DESC LIMIT @rpp OFFSET (@pageNumber - 1) * @rpp";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("companyVehId", NpgsqlTypes.NpgsqlDbType.Uuid, companyVehicleId);
                    command.Parameters.AddWithValue("isActive", true);
                    command.Parameters.AddWithValue("rpp", paging.Rpp);
                    command.Parameters.AddWithValue("pageNumber", paging.PageNumber);

                    var reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var maintenance = new CompanyVehicleMaintenance
                            {
                                Id = Guid.Parse(reader[0].ToString()!),
                                CompanyVehicleId = Guid.Parse(reader[1].ToString()!),
                                Title = reader[2].ToString()!,
                                Description = reader[3].ToString()!
                            };
                            maintenances.Add(maintenance);
                        }
                    }

                    connection.Close();

                    return maintenances;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return maintenances;
            }
        }

        public async Task<int> CountAsync(Guid companyVehicleId)
        {
            int count = 0;
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        "SELECT COUNT(\"Id\") FROM \"CompanyVehicleMaintenance\" WHERE \"CompanyVehicleId\" = @compVehId";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("compVehId", NpgsqlTypes.NpgsqlDbType.Uuid, companyVehicleId);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        int.TryParse(reader[0].ToString(), out count);
                    }
                    else
                    {
                        connection.Close();
                        return 0;
                    }

                    connection.Close();

                    return count;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public async Task<CompanyVehicleMaintenance?> GetCompanyVehicleMaintenanceByIdAsync(Guid id)
        {
            CompanyVehicleMaintenance? maintenance = null;
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        $"SELECT \"Id\", \"CompanyVehicleId\", \"Title\", \"Description\", \"DateCreated\" FROM \"CompanyVehicleMaintenance\" WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    var reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        maintenance = new CompanyVehicleMaintenance
                        {
                            Id = Guid.Parse(reader[0].ToString()!),
                            CompanyVehicleId = Guid.Parse(reader[1].ToString()!),
                            Title = reader[2].ToString()!,
                            Description = reader[3].ToString()!
                        };
                    }

                    connection.Close();

                    return maintenance;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return maintenance;
            }
        }
    }
}