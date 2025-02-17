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
                    var commandText = new StringBuilder("SELECT " +
                        "cv.*, " +
                        "cv.\"DailyPrice\", " +
                        "cv.\"PlateNumber\", " +
                        "cv.\"ImageUrl\", cv.\"IsOperational\", cv.\"IsActive\", " +
                        "c.\"Name\" AS \"CompanyName\", " +
                        "vm.\"Name\" AS \"Model\", " +
                        "vmm.\"Name\" AS \"Make\", " +
                        "vc.\"Name\" AS \"Color\", " +
                        "cl.\"Address\" AS \"LocationAddress\", " +
                        "cl.\"City\" AS \"LocationCity\", " +
                        "u.\"Id\" AS \"CreatedByUserId\", " +
                        "u.\"FullName\" AS \"CreatedByUser\", " +
                        "uu.\"Id\" AS \"UpdatedByUserId\", " +
                        "uu.\"FullName\" AS \"UpdatedByUser\" " +
                        "FROM \"CompanyVehicle\" cv " +
                        "JOIN \"Company\" c ON cv.\"CompanyId\" = c.\"Id\" " +
                        "JOIN \"VehicleModel\" vm ON cv.\"VehicleModelId\" = vm.\"Id\" " +
                        "JOIN \"VehicleMake\" vmm ON vm.\"MakeId\" = vmm.\"Id\" " +
                        "JOIN \"VehicleColor\" vc ON cv.\"ColorId\" = vc.\"Id\" " +
                        "LEFT JOIN \"Location\" cl ON cv.\"CurrentLocationId\" = cl.\"Id\" " +
                        "JOIN \"User\" u ON cv.\"CreatedByUserId\" = u.\"Id\" " +
                        "JOIN \"User\" uu ON cv.\"UpdatedByUserId\" = uu.\"Id\" " +
                        "WHERE 1 = 1");

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

                    ApplyFilters(cmd, commandText, filter);
                    ApplySorting(cmd, commandText, sorting);
                    ApplyPaging(cmd, commandText, paging);

                    cmd.CommandText = commandText.ToString();
                    connection.Open();

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

        private void ApplyFilters(NpgsqlCommand cmd, StringBuilder commandText, CompanyVehicleFilter vehicleFilter)
        {
            if (vehicleFilter.IsActive.HasValue)
            {
                commandText.Append(" AND cv.\"IsActive\" = @isActive");
                cmd.Parameters.AddWithValue("@isActive", vehicleFilter.IsActive.Value);
            }

            if (vehicleFilter.IsOperational.HasValue)
            {
                commandText.Append(" AND cv.\"IsOperational\" = @isOperational");
                cmd.Parameters.AddWithValue("@isOperational", vehicleFilter.IsOperational.Value);
            }

            if (vehicleFilter.CompanyId.HasValue)
            {
                commandText.Append(" AND cv.\"CompanyId\" = @companyId");
                cmd.Parameters.AddWithValue("@companyId", vehicleFilter.CompanyId.Value);
            }

            if (vehicleFilter.VehicleModelId.HasValue)
            {
                commandText.Append(" AND cv.\"VehicleModelId\" = @vehicleModelId");
                cmd.Parameters.AddWithValue("@vehicleModelId", vehicleFilter.VehicleModelId.Value);
            }

            if (!string.IsNullOrWhiteSpace(vehicleFilter.VehicleMakeName))
            {
                commandText.Append(" AND vmm.\"Name\" = @vehicleMakeName");
                cmd.Parameters.AddWithValue("@vehicleMakeName", vehicleFilter.VehicleMakeName);
            }

            if (!string.IsNullOrWhiteSpace(vehicleFilter.VehicleModelName))
            {
                commandText.Append(" AND vm.\"Name\" = @vehicleModelName");
                cmd.Parameters.AddWithValue("@vehicleModelName", vehicleFilter.VehicleModelName);
            }

            if (!string.IsNullOrWhiteSpace(vehicleFilter.ColorName))
            {
                commandText.Append(" AND vc.\"Name\" = @colorName");
                cmd.Parameters.AddWithValue("@colorName", vehicleFilter.ColorName);
            }

            if (vehicleFilter.MinDailyPrice.HasValue)
            {
                commandText.Append(" AND cv.\"DailyPrice\" >= @minDailyPrice");
                cmd.Parameters.AddWithValue("@minDailyPrice", vehicleFilter.MinDailyPrice.Value);
            }

            if (vehicleFilter.MaxDailyPrice.HasValue)
            {
                commandText.Append(" AND cv.\"DailyPrice\" <= @maxDailyPrice");
                cmd.Parameters.AddWithValue("maxDailyPrice", vehicleFilter.MaxDailyPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(vehicleFilter.PlateNumber))
            {
                commandText.Append(" AND cv.\"PlateNumber\" = @plateNumber");
                cmd.Parameters.AddWithValue("@plateNumber", vehicleFilter.PlateNumber);
            }

            if (!string.IsNullOrWhiteSpace(vehicleFilter.ImageUrl))
            {
                commandText.Append(" AND cv.\"ImageUrl\" = @imageUrl");
                cmd.Parameters.AddWithValue("@imageUrl", vehicleFilter.ImageUrl);
            }

            if (vehicleFilter.CurrentLocationId.HasValue)
            {
                commandText.Append(" AND cv.\"CurrentLocationId\" = @currentLocationId");
                cmd.Parameters.AddWithValue("@currentLocationId", vehicleFilter.CurrentLocationId.Value);
            }

            if (!string.IsNullOrWhiteSpace(vehicleFilter.LocationCity))
            {
                commandText.Append(" AND cl.\"City\" = @locationCity");
                cmd.Parameters.AddWithValue("@locationCity", vehicleFilter.LocationCity);
            }

            if (!string.IsNullOrWhiteSpace(vehicleFilter.LocationAddress))
            {
                commandText.Append(" AND cl.\"Address\" ILIKE @locationAddress");
                cmd.Parameters.AddWithValue("@locationAddress", "%" + vehicleFilter.LocationAddress + "%");
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

        public async Task<bool> DeleteCompanyVehicleAsync(Guid compVehId, Guid id)
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

        public async Task<bool> AddCompanyVehicleAsync(CompanyVehicle companyVehicle, Guid userId)
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

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle, Guid userId)
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

                    return await command.ExecuteNonQueryAsync() > 0;
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