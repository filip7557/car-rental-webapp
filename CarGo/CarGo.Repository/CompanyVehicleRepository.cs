using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;
using System.Text;

namespace CarGo.Repository
{
    public class CompanyVehicleRepository : ICompanyVehicleRepository
    {
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb");

        public async Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging,
            CompanyVehicleFilter filter)
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

        private void ApplySorting(NpgsqlCommand cmd, StringBuilder commandText, BookingSorting sorting)
        {
            if (!string.IsNullOrEmpty(sorting.OrderBy))
            {
                var direction = sorting.SortOrder.ToUpper() == "DESC" ? "DESC" : "ASC";
                commandText.Append($" ORDER BY \"{sorting.OrderBy}\" {direction}");
            }
        }

        private void ApplyPaging(NpgsqlCommand cmd, StringBuilder commandText, Paging paging)
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

        public async Task<bool> DeleteCompanyVehicleAsync(Guid compVehId ,Guid id)
        {
            const string selectCommandText = "SELECT \"IsActive\" FROM \"CompanyVehicle\" WHERE \"Id\" = @id";
            const string updateCommandText = "UPDATE \"CompanyVehicle\" SET \"IsActive\" = @newIsActive, \"UpdatedByUserId\" = @updatedByUserId, \"DateUpdated\" = CURRENT_TIMESTAMP WHERE \"Id\" = @id";

            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                // Dohvati trenutni status
                using var selectCommand = new NpgsqlCommand(selectCommandText, connection);
                selectCommand.Parameters.AddWithValue("id", compVehId);
                var currentStatus = await selectCommand.ExecuteScalarAsync();

                if (currentStatus == null)
                {
                    throw new Exception("Company Vehicle not found");
                }

                bool newIsActive = !(bool)currentStatus; // Ako je trenutno aktivno, postavi na neaktivno (false)

                // Ažuriraj status
                using var updateCommand = new NpgsqlCommand(updateCommandText, connection);
                updateCommand.Parameters.AddWithValue("id", compVehId);
                updateCommand.Parameters.AddWithValue("newIsActive", newIsActive);
                updateCommand.Parameters.AddWithValue("updatedByUserId", id);

                // Provjeri ako je izvršen upit
                return await updateCommand.ExecuteNonQueryAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deactivating company vehicle", ex);
            }
        }

        public async Task AddCompanyVehicleAsync(CompanyVehicle companyVehicle, Guid userId)
        {
            string commandText =
                "INSERT INTO \"CompanyVehicle\" (\"Id\", \"CompanyId\", \"VehicleModelId\", \"DailyPrice\", \"ColorId\", \"PlateNumber\", \"ImageUrl\", \"CurrentLocationId\", \"IsOperational\", \"IsActive\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
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
                    command.Parameters.AddWithValue("@currentLocationId",
                        companyVehicle.CurrentLocationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@isOperational", companyVehicle.IsOperational);
                    command.Parameters.AddWithValue("@isActive", companyVehicle.IsActive);
                    command.Parameters.AddWithValue("@createdByUserId", userId);
                    command.Parameters.AddWithValue("@updatedByUserId", userId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle, Guid userId)
        {
            string commandText =
                "UPDATE \"CompanyVehicle\" SET \"CompanyId\" = @companyId, \"VehicleModelId\" = @vehicleModelId, \"DailyPrice\" = @dailyPrice, \"ColorId\" = @colorId, \"PlateNumber\" = @plateNumber, \"ImageUrl\" = @imageUrl, \"CurrentLocationId\" = @currentLocationId, \"IsOperational\" = @isOperational, \"IsActive\" = @isActive, \"UpdatedByUserId\" = @updatedByUserId, \"DateUpdated\" = CURRENT_TIMESTAMP WHERE \"Id\" = @id";

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
                    command.Parameters.AddWithValue("@imageUrl",
                        updatedCompanyVehicle.ImageUrl ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@currentLocationId",
                        updatedCompanyVehicle.CurrentLocationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@isOperational", updatedCompanyVehicle.IsOperational);
                    command.Parameters.AddWithValue("@isActive", updatedCompanyVehicle.IsActive);
                    command.Parameters.AddWithValue("@updatedByUserId", userId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public CompanyVehicle ReadCompanyVehicle(NpgsqlDataReader reader)
        {
            return new CompanyVehicle
            {
                Id = Guid.Parse(reader["Id"].ToString()!),
                CompanyId = Guid.Parse(reader["CompanyId"].ToString()!),
                VehicleModelId = Guid.Parse(reader["VehicleModelId"].ToString()!),
                DailyPrice = decimal.Parse(reader["DailyPrice"].ToString()!),
                ColorId = Guid.Parse(reader["ColorId"].ToString()!),
                PlateNumber = reader["PlateNumber"].ToString()!,
                ImageUrl = reader["ImageUrl"] == DBNull.Value ? null : reader["ImageUrl"].ToString(),
                CurrentLocationId = reader["CurrentLocationId"] == DBNull.Value
                    ? (Guid?)null
                    : Guid.Parse(reader["CurrentLocationId"].ToString()!),
                IsOperational = bool.Parse(reader["IsOperational"].ToString()!),
                IsActive = bool.Parse(reader["IsActive"].ToString()!),
                CreatedByUserId = Guid.Parse(reader["CreatedByUserId"].ToString()!),
                UpdatedByUserId = Guid.Parse(reader["UpdatedByUserId"].ToString()!),
                DateCreated = DateTime.Parse(reader["DateCreated"].ToString()!),
                DateUpdated = DateTime.Parse(reader["DateCreated"].ToString()!)
            };
        }
    }
}