using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;
using System.ComponentModel.Design;
using System.Text;

namespace CarGo.Repository
{
    public class BookingRepository : IBookRepository
    {
        private string? _connectionString;

        public BookingRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<List<Booking>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging, BookingFilter filter)
        {
            var bookings = new List<Booking>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

                    var commandText = new StringBuilder(@"
                SELECT
                    b.*,
                    bs.""Name"" AS ""BookingStatus"",
                    c.""Name"" AS ""CompanyName"",
                    vm.""Name"" AS ""Model"",
                    vmm.""Name"" AS ""Make"",
                    vc.""Name"" AS ""Color"",
                    cv.""DailyPrice"",
                    cv.""PlateNumber"",
                    cv.""ImageUrl"",
                    cl.""Address"" AS ""LocationAddress"",
                    cl.""City"" AS ""LocationCity"",
                    u.""Id"" AS ""UserId"",
                    u.""FullName"",
                    u.""Email"",
                    u.""PhoneNumber"",
                    r.""Name"" AS ""UserRole""
                FROM ""Booking"" b
                JOIN ""BookingStatus"" bs ON b.""StatusId"" = bs.""Id""
                JOIN ""CompanyVehicle"" cv ON b.""CompanyVehicleId"" = cv.""Id""
                JOIN ""Company"" c ON cv.""CompanyId"" = c.""Id""
                JOIN ""VehicleModel"" vm ON cv.""VehicleModelId"" = vm.""Id""
                JOIN ""VehicleMake"" vmm ON vm.""MakeId"" = vmm.""Id""
                JOIN ""VehicleColor"" vc ON cv.""ColorId"" = vc.""Id""
                JOIN ""Location"" cl ON cv.""CurrentLocationId"" = cl.""Id""
                JOIN ""User"" u ON b.""UserId"" = u.""Id""
                JOIN ""Role"" r ON u.""RoleId"" = r.""Id""
                WHERE 1 = 1");


                
                    if (filter.UserRole == "User")
                    {
                        commandText.Append(" AND b.\"UserId\" = @userId");
                        cmd.Parameters.AddWithValue("userId", filter.UserId);
                    }
             
                    else if (filter.UserRole == "Manager")
                    {
                        commandText.Append(@"
                    AND cv.""CompanyId"" IN (SELECT ""CompanyId"" FROM ""UserCompany"" WHERE ""UserId"" = @userId)");
                        cmd.Parameters.AddWithValue("userId", filter.UserId);
                    }
                   
                    else if (filter.UserRole == "Administrator")
                    {
                        if (filter.CompanyId.HasValue)
                        {
                            commandText.Append(" AND cv.\"CompanyId\" = @companyId");
                            cmd.Parameters.AddWithValue("companyId", filter.CompanyId.Value);
                        }
                    }

                   
                    if (filter.IsActive.HasValue)
                    {
                        commandText.Append(" AND b.\"IsActive\" = @isActive");
                        cmd.Parameters.AddWithValue("isActive", filter.IsActive.Value);
                    }

                    ApplyFilters(cmd, commandText, filter);
                    ApplySorting(cmd, commandText, sorting);
                    ApplyPaging(cmd, commandText, paging);

                    cmd.CommandText = commandText.ToString();
                    await connection.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bookings.Add(ReadBooking(reader));
                        }
                    }
                }
            }

            return bookings;
        }


        private void ApplySorting(NpgsqlCommand cmd, StringBuilder commandText, BookingSorting sorting)
        {
            if (!string.IsNullOrEmpty(sorting.OrderBy))
            {
                var direction = sorting.SortOrder.ToUpper();
                commandText.Append($" ORDER BY b.\"{sorting.OrderBy}\" {direction}");
            }
        }

        private void ApplyPaging(NpgsqlCommand cmd, StringBuilder commandText, BookingPaging paging)
        {
            if (paging.PageNumber > 0)
            {
                commandText.Append(" OFFSET @OFFSET LIMIT @ROWS");
                cmd.Parameters.AddWithValue("@OFFSET", (paging.PageNumber - 1) * paging.Rpp);
                cmd.Parameters.AddWithValue("@ROWS", paging.Rpp);
            }
        }

        private void ApplyFilters(NpgsqlCommand cmd, StringBuilder commandText, BookingFilter bookingFilter)
        {
            if (bookingFilter.IsActive.HasValue)
            {
                commandText.Append(" AND b.\"IsActive\" = @isActive");
                cmd.Parameters.AddWithValue("@isActive", bookingFilter.IsActive.Value);
            }

            if (bookingFilter.UserId.HasValue)
            {
                commandText.Append(" AND b.\"UserId\" = @userId");
                cmd.Parameters.AddWithValue("@userId", bookingFilter.UserId.Value);
            }

            if (bookingFilter.CompanyVehicleId.HasValue)
            {
                commandText.Append(" AND b.\"CompanyVehicleId\" = @companyVehicleId");
                cmd.Parameters.AddWithValue("@companyVehicleId", bookingFilter.CompanyVehicleId.Value);
            }

            if (bookingFilter.StatusId.HasValue)
            {
                commandText.Append(" AND b.\"StatusId\" = @statusId");
                cmd.Parameters.AddWithValue("@statusId", bookingFilter.StatusId.Value);
            }

            if (bookingFilter.PickUpLocationId.HasValue)
            {
                commandText.Append(" AND b.\"PickUpLocationId\" = @pickUpLocationId");
                cmd.Parameters.AddWithValue("@pickUpLocationId", bookingFilter.PickUpLocationId.Value);
            }

            if (bookingFilter.DropOffLocationId.HasValue)
            {
                commandText.Append(" AND b.\"DropOffLocationId\" = @dropOffLocationId");
                cmd.Parameters.AddWithValue("@dropOffLocationId", bookingFilter.DropOffLocationId.Value);
            }

            if (bookingFilter.StartDate.HasValue)
            {
                commandText.Append(" AND b.\"StartDate\" >= @startDate");
                cmd.Parameters.AddWithValue("@startDate", bookingFilter.StartDate.Value);
            }

            if (bookingFilter.EndDate.HasValue)
            {
                commandText.Append(" AND b.\"EndDate\" <= @endDate");
                cmd.Parameters.AddWithValue("@endDate", bookingFilter.EndDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(bookingFilter.BookingStatusName))
            {
                commandText.Append(" AND bs.\"Name\" = @bookingStatusName");
                cmd.Parameters.AddWithValue("@bookingStatusName", bookingFilter.BookingStatusName);
            }

            if (!string.IsNullOrWhiteSpace(bookingFilter.VehicleMakeName))
            {
                commandText.Append(" AND vmm.\"Name\" = @vehicleMakeName");
                cmd.Parameters.AddWithValue("@vehicleMakeName", bookingFilter.VehicleMakeName);
            }

            if (!string.IsNullOrWhiteSpace(bookingFilter.VehicleModelName))
            {
                commandText.Append(" AND vm.\"Name\" = @vehicleModelName");
                cmd.Parameters.AddWithValue("@vehicleModelName", bookingFilter.VehicleModelName);
            }

            if (!string.IsNullOrWhiteSpace(bookingFilter.ImageUrl))
            {
                commandText.Append(" AND cv.\"ImageUrl\" = @imageUrl");
                cmd.Parameters.AddWithValue("@imageUrl", bookingFilter.ImageUrl);
            }

            if (bookingFilter.StatusId.HasValue)
            {
                commandText.Append(" AND \"StatusId\" = @statusId");
                cmd.Parameters.AddWithValue("@statusId", bookingFilter.StatusId.Value);
            }

            if (bookingFilter.PickUpLocationId.HasValue)
            {
                commandText.Append(" AND \"PickUpLocationId\" = @pickUpLocationId");
                cmd.Parameters.AddWithValue("@pickUpLocationId", bookingFilter.PickUpLocationId.Value);
            }

            if (bookingFilter.DropOffLocationId.HasValue)
            {
                commandText.Append(" AND \"DropOffLocationId\" = @dropOffLocationId");
                cmd.Parameters.AddWithValue("@dropOffLocationId", bookingFilter.DropOffLocationId.Value);
            }

            if (bookingFilter.StartDate.HasValue)
            {
                commandText.Append(" AND \"StartDate\" >= @startDate");
                cmd.Parameters.AddWithValue("@startDate", bookingFilter.StartDate.Value);
            }

            if (bookingFilter.EndDate.HasValue)
            {
                commandText.Append(" AND \"EndDate\" <= @endDate");
                cmd.Parameters.AddWithValue("@endDate", bookingFilter.EndDate.Value);
            }
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            string commandText = "SELECT * FROM \"Booking\" WHERE \"Id\" = @id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return ReadBooking(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task SoftDeleteBookingAsync(Guid id)
        {
            string commandText = "UPDATE \"Booking\" SET \"IsActive\" = FALSE WHERE  \"Id\" = @id";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddBookingAsync(Booking booking, Guid createdByUserId)
        {
            string commandText =
                "INSERT INTO \"Booking\" (\"Id\", \"UserId\", \"CompanyVehicleId\", \"StartDate\", \"EndDate\", " +
                "\"TotalPrice\", \"StatusId\", \"PickUpLocationId\", \"DropOffLocationId\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                "VALUES (@id, @userId, @companyVehicleId, @startDate, @endDate, " +
                "@totalPrice, @statusId, @pickUpLocationId, @dropOffLocationId, @createdByUserId, @updatedByUserId)";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@id", booking.Id);
                    command.Parameters.AddWithValue("@userId", createdByUserId);
                    command.Parameters.AddWithValue("@companyVehicleId", booking.CompanyVehicleId);
                    command.Parameters.AddWithValue("@startDate", booking.StartDate);
                    command.Parameters.AddWithValue("@endDate", booking.EndDate);
                    command.Parameters.AddWithValue("@totalPrice", booking.TotalPrice);
                    command.Parameters.AddWithValue("@statusId", booking.StatusId);
                    command.Parameters.AddWithValue("@pickUpLocationId", booking.PickUpLocationId);
                    command.Parameters.AddWithValue("@dropOffLocationId", booking.DropOffLocationId);
                    command.Parameters.AddWithValue("@createdByUserId", createdByUserId);
                    command.Parameters.AddWithValue("@updatedByUserId", createdByUserId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateBookingAsync(Guid id, Booking updatedBooking, Guid createdByUserId)
        {
            string commandText = "UPDATE \"Booking\" SET " +
                                 "\"IsActive\" = @isActive, " +
                                 "\"UserId\" = @userId, " +
                                 "\"CompanyVehicleId\" = @companyVehicleId, " +
                                 "\"StartDate\" = @startDate, " +
                                 "\"EndDate\" = @endDate, " +
                                 "\"TotalPrice\" = @totalPrice, " +
                                 "\"StatusId\" = @statusId, " +
                                 "\"PickUpLocationId\" = @pickUpLocationId, " +
                                 "\"DropOffLocationId\" = @dropOffLocationId, " +
                                 "\"UpdatedByUserId\" = @updatedByUserId, " +
                                 "\"DateUpdated\" = CURRENT_TIMESTAMP " +
                                 "WHERE \"Id\" = @id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@isActive", updatedBooking.IsActive);
                    command.Parameters.AddWithValue("@userId", createdByUserId);
                    command.Parameters.AddWithValue("@companyVehicleId", updatedBooking.CompanyVehicleId);
                    command.Parameters.AddWithValue("@startDate", updatedBooking.StartDate);
                    command.Parameters.AddWithValue("@endDate", updatedBooking.EndDate);
                    command.Parameters.AddWithValue("@totalPrice", updatedBooking.TotalPrice);
                    command.Parameters.AddWithValue("@statusId", updatedBooking.StatusId);
                    command.Parameters.AddWithValue("@pickUpLocationId", updatedBooking.PickUpLocationId);
                    command.Parameters.AddWithValue("@dropOffLocationId", updatedBooking.DropOffLocationId);
                    command.Parameters.AddWithValue("@updatedByUserId", createdByUserId);
                    command.Parameters.AddWithValue("@id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateBookingStatusAsync(Guid bookingId, BookingStatus status, Guid updatedByUserId)
        {
            string commandText = @"UPDATE ""Booking"" SET ""StatusId"" = @statusId,""UpdatedByUserId"" = @updatedByUserId WHERE ""Id"" = @bookingId";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@statusId", status.ID);
                    command.Parameters.AddWithValue("@updatedByUserId", updatedByUserId);
                    command.Parameters.AddWithValue("@bookingId", bookingId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public Booking ReadBooking(NpgsqlDataReader reader)
        {
            return new Booking
            {
                Id = Guid.Parse(reader["Id"].ToString()!),
                IsActive = bool.Parse(reader["IsActive"].ToString()!),
                UserId = Guid.Parse(reader["UserId"].ToString()!),
                CompanyVehicleId = Guid.Parse(reader["CompanyVehicleId"].ToString()!),
                StartDate = DateTime.Parse(reader["StartDate"].ToString()!),
                EndDate = DateTime.Parse(reader["EndDate"].ToString()!),
                TotalPrice = decimal.Parse(reader["TotalPrice"].ToString()!),
                StatusId = Guid.Parse(reader["StatusId"].ToString()!),
                PickUpLocationId = Guid.Parse(reader["PickUpLocationId"].ToString()!),
                DropOffLocationId = Guid.Parse(reader["DropOffLocationId"].ToString()!),
                DateCreated = DateTime.Parse(reader["DateCreated"].ToString()!),
                DateUpdated = DateTime.Parse(reader["DateUpdated"].ToString()!),
                CreatedByUserId = Guid.Parse(reader["CreatedByUserId"].ToString()!),
                UpdatedByUserId = Guid.Parse(reader["UpdatedByUserId"].ToString()!)
            };
        }
    }
}