using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace Repository
{
    public class BookingStatusRepository : IBookingStatusRepository
    {
        private string? _connectionString;
        public BookingStatusRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }


        //GET ALL
        public async Task<List<BookingStatus>> GetAllAsync()
        {
            var bookingStatuses = new List<BookingStatus>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\"" +
                                      "FROM \"BookingStatus\"";

                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var bookingStatus = new BookingStatus()
                            {
                                ID = Guid.Parse(reader[0].ToString()!),
                                Name = reader[1].ToString()!,
                            };

                            bookingStatuses.Add(bookingStatus);
                        }
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return bookingStatuses;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //GET BY ID
        public async Task<BookingStatus?> GetByIdAsync(Guid id)
        {
            try
            {
                var bookingStatus = new BookingStatus() { };

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT " +
                                      "\"BookingStatus\".\"Id\", \"Name\"" +
                                      "FROM \"BookingStatus\" " +
                                      "WHERE \"BookingStatus\".\"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        bookingStatus.ID = Guid.Parse(reader[0].ToString()!);
                        bookingStatus.Name = reader[1].ToString()!;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return bookingStatus;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}