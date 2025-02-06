
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
                    string commandText = "INSERT INTO" +
                        " \"DamageReport\"" +
                        " (\"Id\", \"Title\", \"Description\", \"BookingId\", \"CreatedByUserId\", \"UpdatedByUserId\"" +
                        " VALUES" +
                        " (@id, @title, @desc, @bookingId, @createdBy, @updatedBy);";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", damageReport.Id);
                    command.Parameters.AddWithValue("title", damageReport.Title);
                    command.Parameters.AddWithValue("desc", damageReport.Description);
                    command.Parameters.AddWithValue("bookingId", damageReport.BookingId);
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
    }
}
