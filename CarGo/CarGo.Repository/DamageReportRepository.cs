
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class DamageReportRepository : IDamageReportRepository
    {
        private string? _connectionString;

        public DamageReportRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
            ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<bool> CreateDamageReportAsync(DamageReport damageReport, Guid createdByUserId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "INSERT INTO \"DamageReport\"" +
                        " (\"Id\", \"Title\", \"Description\", \"BookingId\", \"CreatedByUserId\", \"UpdatedByUserId\")" +
                        " VALUES" +
                        " (@id, @title, @desc, @bookingId, @createdBy, @updatedBy);";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, damageReport.Id);
                    command.Parameters.AddWithValue("title", damageReport.Title);
                    command.Parameters.AddWithValue("desc", damageReport.Description);
                    command.Parameters.AddWithValue("bookingId", NpgsqlTypes.NpgsqlDbType.Uuid, damageReport.BookingId);
                    command.Parameters.AddWithValue("createdBy", createdByUserId);
                    command.Parameters.AddWithValue("updatedBy", createdByUserId);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    connection.Close();

                    if (affectedRows == 0)
                        return false;

                    return true;
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

        public async Task<DamageReport?> GetDamageReportByCompanyVehicleIdAsync(Guid companyVehicleId, bool isAdmin)
        {
            DamageReport? damageReport = null;
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT \"DamageReport\".\"Id\", \"DamageReport\".\"Title\", \"DamageReport\".\"Description\", \"DamageReport\".\"BookingId\"" +
                        " FROM \"DamageReport\"" +
                        " INNER JOIN \"Booking\" ON \"DamageReport\".\"BookingId\" = \"Booking\".\"Id\"" +
                        " INNER JOIN \"CompanyVehicle\" ON \"Booking\".\"CompanyVehicleId\" = \"CompanyVehicle\".\"Id\"" +
                        $" WHERE \"CompanyVehicle\".\"Id\" = @id AND {(!isAdmin ? "IsActive = @value" : "")};";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, companyVehicleId);
                    command.Parameters.AddWithValue("value", true);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        damageReport = new DamageReport
                        {
                            Id = Guid.Parse(reader[0].ToString()!),
                            Title = reader[1].ToString()!,
                            Description = reader[2].ToString()!,
                            BookingId = Guid.Parse(reader[3].ToString()!),
                        };
                    }

                    return damageReport;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return damageReport;
            }
        }
        public async Task<bool> DeleteDamageReportAsync(Guid damageReportId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "UPDATE \"DamageReport\" set \"IsActive\" = @isActive WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("isActive", false);
                    command.Parameters.AddWithValue("id", damageReportId);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();

                    connection.Close();

                    if (affectedRows != 0)
                        return true;

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }
    }
}
