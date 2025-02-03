using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;
using System.Text;

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
        public async Task<bool> SaveCompanyVehicleMaintenanceAsync(CompanyVehicleMaintenance maintenance, Guid createdByUserId)
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
        public async Task<List<CompanyVehicleMaintenance>> GetMaintenancesByCompanyVehicleIdAsync(Guid companyVehicleId, Paging paging)
        {
            var maintenances = new List<CompanyVehicleMaintenance>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT \"Id\", \"CompanyVehicleId\", \"Title\", \"Description\" \"DateCreated\" FROM \"CompanyVehicleMaintenance\" WHERE \"CompanyVehicleId\" = @companyVehId ORDER BY \"DateCreated\" DESC LIMIT @rpp OFFSET (@pageNumber - 1) * @rpp";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("companyVehId", NpgsqlTypes.NpgsqlDbType.Uuid, companyVehicleId);
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
    }
}
