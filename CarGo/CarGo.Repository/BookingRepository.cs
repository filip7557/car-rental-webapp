using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarGo.Repository
{
    public class BookingRepository : IBookRepository
    {
        private  string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");


        public async Task<List<Booking>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging, BookingFilter filter)
        {
            var bookings = new List<Booking>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

                    var commandText = new StringBuilder("SELECT * FROM \"Booking\" WHERE 1=1");
                    
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
                var direction = sorting.SortOrder.ToUpper() == "DESC" ? "DESC" : "ASC";
                commandText.Append($" ORDER BY \"{sorting.OrderBy}\" {direction}");
            }
        }

        private void ApplyPaging(NpgsqlCommand cmd, StringBuilder commandText, BookingPaging paging)
        {
            if (paging.PageNumber > 0)
            {
                commandText.Append(" OFFSET @OFFSET FETCH NEXT @ROWS ROWS ONLY;");
                cmd.Parameters.AddWithValue("@OFFSET", (paging.PageNumber - 1) * paging.Rpp);
                cmd.Parameters.AddWithValue("@ROWS", paging.Rpp);
            }
        }


        private void ApplyFilters(NpgsqlCommand cmd, StringBuilder commandText, BookingFilter bookingFilter)
        {
            if (bookingFilter.IsActive.HasValue)
            {
                commandText.Append(" AND \"IsActive\" = @isActive");
                cmd.Parameters.AddWithValue("@isActive", bookingFilter.IsActive.Value);
            }

            if (bookingFilter.UserId.HasValue)
            {
                commandText.Append(" AND \"UserId\" = @userId");
                cmd.Parameters.AddWithValue("@userId", bookingFilter.UserId.Value);
            }

            if (bookingFilter.CompanyVehicleId.HasValue)
            {
                commandText.Append(" AND \"CompanyVehicleId\" = @companyVehicleId");
                cmd.Parameters.AddWithValue("@companyVehicleId", bookingFilter.CompanyVehicleId.Value);
            }
        }


        public async Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            string commandText = "SELECT * FROM \"Booking\" WHERE \"Id\" = @id";

            using (var connection = new NpgsqlConnection(connectionString))
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

        public async Task DeleteBookingAsync(Guid id)
        {
            string commandText = "DELETE FROM \"Booking\" WHERE \"Id\" = @id";
            using (var connection= new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using(var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddBookingAsync(Booking booking)
        {
            string commandText = "INSERT INTO \"Booking\" (\"IsActive\", \"UserId\", \"CompanyVehicleId\", \"StartDate\", \"EndDate\", " +
                                 "\"TotalPrice\", \"StatusId\", \"PickUpLocationId\", \"DropOffLocationId\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                                 "VALUES (@isActive, @userId, @companyVehicleId, @startDate, @endDate, " +
                                 "@totalPrice, @statusId, @pickUpLocationId, @dropOffLocationId, @createdByUserId, @updatedByUserId)";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(commandText, connection))
                {
                 
                    command.Parameters.AddWithValue("@isActive", booking.IsActive);
                    command.Parameters.AddWithValue("@userId", booking.UserId);
                    command.Parameters.AddWithValue("@companyVehicleId", booking.CompanyVehicleId);
                    command.Parameters.AddWithValue("@startDate", booking.StartDate); 
                    command.Parameters.AddWithValue("@endDate", booking.EndDate);     
                    command.Parameters.AddWithValue("@totalPrice", booking.TotalPrice);
                    command.Parameters.AddWithValue("@statusId", booking.StatusId);
                    command.Parameters.AddWithValue("@pickUpLocationId", booking.PickUpLocationId);
                    command.Parameters.AddWithValue("@dropOffLocationId", booking.DropOffLocationId);
                    command.Parameters.AddWithValue("@createdByUserId", booking.CreatedByUserId);
                    command.Parameters.AddWithValue("@updatedByUserId", booking.UpdatedByUserId);

                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task UpdateBookingAsync(Guid id,Booking updatedBooking)
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

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(commandText, connection))
                {
           
                    command.Parameters.AddWithValue("@isActive", updatedBooking.IsActive);
                    command.Parameters.AddWithValue("@userId", updatedBooking.UserId);
                    command.Parameters.AddWithValue("@companyVehicleId", updatedBooking.CompanyVehicleId);
                    command.Parameters.AddWithValue("@startDate", updatedBooking.StartDate);
                    command.Parameters.AddWithValue("@endDate", updatedBooking.EndDate);
                    command.Parameters.AddWithValue("@totalPrice", updatedBooking.TotalPrice);
                    command.Parameters.AddWithValue("@statusId", updatedBooking.StatusId);
                    command.Parameters.AddWithValue("@pickUpLocationId", updatedBooking.PickUpLocationId);
                    command.Parameters.AddWithValue("@dropOffLocationId", updatedBooking.DropOffLocationId);
                    command.Parameters.AddWithValue("@updatedByUserId", updatedBooking.UpdatedByUserId);
                    command.Parameters.AddWithValue("@id", id); 

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
