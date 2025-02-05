using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class CompanyVehicleRepository : ICompanyVehicleRepository
    {
        private string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public async Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging, CompanyVehicleFilter filter)
        {
            var vehicles = new List<CompanyVehicle>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    var commandText = new StringBuilder("SELECT * FROM \"CompanyVehicle\" WHERE 1=1");

                    ApplyFilters(cmd, commandText, filter);
                    ApplySorting(cmd, commandText, sorting);
                    ApplyPaging(cmd, commandText, paging);

                    cmd.CommandText = commandText.ToString();
                    await connection.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            vehicles.Add(ReadCompanyVehicle(reader));
                        }
                    }
                }
            }
            return vehicles;
        }

        private void ApplySorting(NpgsqlCommand cmd, StringBuilder commandText, CompanyVehicleSorting sorting)
        {
            if (!string.IsNullOrEmpty(sorting.OrderBy))
            {
                var direction = sorting.SortOrder.ToUpper() == "DESC" ? "DESC" : "ASC";
                commandText.Append($" ORDER BY \"{sorting.OrderBy}\" {direction}");
            }
        }

        private void ApplyPaging(NpgsqlCommand cmd, StringBuilder commandText, CompanyVehiclePaging paging)
        {
            if (paging.PageNumber > 0)
            {
                commandText.Append(" OFFSET @OFFSET FETCH NEXT @ROWS ROWS ONLY;");
                cmd.Parameters.AddWithValue("@OFFSET", (paging.PageNumber - 1) * paging.Rpp);
                cmd.Parameters.AddWithValue("@ROWS", paging.Rpp);
            }
        }

        private void ApplyFilters(NpgsqlCommand cmd, StringBuilder commandText, CompanyVehicleFilter filter)
        {
            if (filter.IsActive.HasValue)
            {
                commandText.Append(" AND \"IsActive\" = @isActive");
                cmd.Parameters.AddWithValue("@isActive", filter.IsActive.Value);
            }
        }

        public async Task<CompanyVehicle?> GetCompanyVehicleByIdAsync(Guid id)
        {
            string commandText = "SELECT * FROM \"CompanyVehicle\" WHERE \"Id\" = @id";

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
                            return ReadCompanyVehicle(reader);
                        }
                    }
                }
            }
            return null;
        }

        public async Task DeleteCompanyVehicleAsync(Guid id)
        {
            string commandText = "DELETE FROM \"CompanyVehicle\" WHERE \"Id\" = @id";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddCompanyVehicleAsync(CompanyVehicle companyVehicle)
        {
            string commandText = "INSERT INTO \"CompanyVehicle\" (\"Id\", \"CompanyId\", \"VehicleModelId\", \"DailyPrice\", \"ColorId\", \"PlateNumber\", \"ImageUrl\", \"CurrentLocationId\", \"IsOperational\", \"IsActive\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                                 "VALUES (@id, @companyId, @vehicleModelId, @dailyPrice, @colorId, @plateNumber, @imageUrl, @currentLocationId, @isOperational, @isActive, @createdByUserId, @updatedByUserId)";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@id", companyVehicle.Id);
                    command.Parameters.AddWithValue("@companyId", companyVehicle.CompanyId);
                    command.Parameters.AddWithValue("@vehicleModelId", companyVehicle.VehicleModelId);
                    command.Parameters.AddWithValue("@dailyPrice", companyVehicle.DailyPrice);
                    command.Parameters.AddWithValue("@colorId", companyVehicle.ColorId);
                    command.Parameters.AddWithValue("@plateNumber", companyVehicle.PlateNumber);
                    command.Parameters.AddWithValue("@imageUrl", companyVehicle.ImageUrl ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@currentLocationId", companyVehicle.CurrentLocationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@isOperational", companyVehicle.IsOperational);
                    command.Parameters.AddWithValue("@isActive", companyVehicle.IsActive);
                    command.Parameters.AddWithValue("@createdByUserId", companyVehicle.CreatedByUserId);
                    command.Parameters.AddWithValue("@updatedByUserId", companyVehicle.UpdatedByUserId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle)
        {
            string commandText = "UPDATE \"CompanyVehicle\" SET \"CompanyId\" = @companyId, \"VehicleModelId\" = @vehicleModelId, \"DailyPrice\" = @dailyPrice, \"ColorId\" = @colorId, \"PlateNumber\" = @plateNumber, \"ImageUrl\" = @imageUrl, \"CurrentLocationId\" = @currentLocationId, \"IsOperational\" = @isOperational, \"IsActive\" = @isActive, \"UpdatedByUserId\" = @updatedByUserId, \"DateUpdated\" = CURRENT_TIMESTAMP WHERE \"Id\" = @id";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@companyId", updatedCompanyVehicle.CompanyId);
                    command.Parameters.AddWithValue("@vehicleModelId", updatedCompanyVehicle.VehicleModelId);
                    command.Parameters.AddWithValue("@dailyPrice", updatedCompanyVehicle.DailyPrice);
                    command.Parameters.AddWithValue("@colorId", updatedCompanyVehicle.ColorId);
                    command.Parameters.AddWithValue("@plateNumber", updatedCompanyVehicle.PlateNumber);
                    command.Parameters.AddWithValue("@imageUrl", updatedCompanyVehicle.ImageUrl ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@currentLocationId", updatedCompanyVehicle.CurrentLocationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@isOperational", updatedCompanyVehicle.IsOperational);
                    command.Parameters.AddWithValue("@isActive", updatedCompanyVehicle.IsActive);
                    command.Parameters.AddWithValue("@updatedByUserId", updatedCompanyVehicle.UpdatedByUserId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
