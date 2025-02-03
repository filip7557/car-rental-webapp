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
        public async Task<bool> SaveCompanyVehicleMaintenance(CompanyVehicleMaintenance maintenance, Guid createdByUserId)
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
    }
}
