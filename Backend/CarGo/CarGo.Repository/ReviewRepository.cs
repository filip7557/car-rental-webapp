using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Repository
{
    public class ReviewRepository: IReviewRepository
    {
        private string? _connectionString;


        public ReviewRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<List<Review>> GetReviewsByCompanyIdAsync(Guid companyId, bool isAdmin)
        {
            var reviews = new List<Review>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

      
                    var commandText = new StringBuilder(@"
                SELECT r.*
                FROM ""Review"" r
                INNER JOIN ""Booking"" b ON r.""BookingId"" = b.""Id""
                INNER JOIN ""CompanyVehicle"" cv ON b.""CompanyVehicleId"" = cv.""Id""
                WHERE cv.""CompanyId"" = @companyId");

                    if (!isAdmin)
                    {
                        
                        commandText.Append(" AND r.\"IsActive\" = @isActive");
                        cmd.Parameters.AddWithValue("@isActive", true);
                    }
              

                    cmd.CommandText = commandText.ToString();
                    cmd.Parameters.AddWithValue("@companyId", companyId);

              

                    await connection.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reviews.Add(ReadReviews(reader)); 
                        }
                    }
                }
            }

            return reviews;
        }


        public async Task<bool> AddReviewAsync(Review review, Guid createdByUserId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "INSERT INTO \"Review\"" +
                        " (\"Id\", \"Title\", \"Description\", \"BookingId\", \"CreatedByUserId\", \"UpdatedByUserId\")" +
                        " VALUES" +
                        " (@id, @title, @desc, @bookingId, @createdBy, @updatedBy);";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, review.Id);
                    command.Parameters.AddWithValue("title", review.Title);
                    command.Parameters.AddWithValue("desc", review.Description);
                    command.Parameters.AddWithValue("bookingId", NpgsqlTypes.NpgsqlDbType.Uuid, review.BookingId);
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


        public async Task<bool> SoftDeleteReviewAsync(Guid damageReportId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "UPDATE \"Review\" set \"IsActive\" = @isActive WHERE \"Id\" = @id;";
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

        public Review ReadReviews(NpgsqlDataReader reader)
        {
            return new Review
            {
                Id = Guid.Parse(reader["Id"].ToString()!),
                BookingId = Guid.Parse(reader["BookingId"].ToString()!),
                Title = Convert.ToString(reader["Title"]) ?? string.Empty,
                Description = Convert.ToString(reader["Description"]) ?? string.Empty,
                IsActive = bool.Parse(reader["IsActive"].ToString()!),
                DateCreated = DateTime.Parse(reader["DateCreated"].ToString()!),
                DateUpdated = DateTime.Parse(reader["DateUpdated"].ToString()!),
                CreatedByUserId = Guid.Parse(reader["CreatedByUserId"].ToString()!),
                UpdatedByUserId = Guid.Parse(reader["UpdatedByUserId"].ToString()!),
            };
        }


    }
}
