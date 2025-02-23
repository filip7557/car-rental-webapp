using CarGo.Model;
using CarGo.Repository.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Text;

namespace CarGo.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private string _connectionString;
        protected readonly string TableName = "\"Location\"";

        public LocationRepository(IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb");
        }

        public async Task<List<Location>> GetLocationAsync()
        {
            var locationList = new List<Location>();
            StringBuilder query =
                new StringBuilder(
                    $"SELECT \"Id\", \"Address\", \"City\", \"Country\" FROM \"Location\" Where \"IsActive\" = true");
            using (var connection = new NpgsqlConnection(_connectionString))
            using (var command = new NpgsqlCommand(query.ToString(), connection))
            {
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var location = new Location
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        City = reader.GetString(reader.GetOrdinal("City")),
                        Country = reader.GetString(reader.GetOrdinal("Country")),
                        
                    };
                    locationList.Add(location);
                }

                return locationList;
            }
        }



        public async Task<Location> GetByIdLocationAsync(Guid id)
        {
            string commandText =
                $"SELECT \"Id\", \"Address\", \"City\", \"Country\" FROM \"Location\" WHERE \"Id\" = @id ";
            using (var connection = new NpgsqlConnection(_connectionString))
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                await connection.OpenAsync();
                command.Parameters.AddWithValue("id", id);
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Location
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        City = reader.GetString(reader.GetOrdinal("City")),
                        Country = reader.GetString(reader.GetOrdinal("Country")),
                    };
                }
            }

            throw new Exception("Location with inputed id not found");
        }

        public async Task<bool> PostLocationAsync(Location entity, Guid userId)
        {
            string commandText =
                $"INSERT INTO {TableName} (\"Id\", \"Address\", \"City\", \"Country\", \"IsActive\",\"CreatedByUserId\", \"UpdatedByUserId\") VALUES (@id, @address, @city, @country, @isActive,@createdByUserId, @UpdatedByUserId)";
            using (var connection = new NpgsqlConnection(_connectionString))
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                await connection.OpenAsync();
                command.Parameters.AddWithValue("id", entity.Id);
                command.Parameters.AddWithValue("address", entity.Address);
                command.Parameters.AddWithValue("city", entity.City);
                command.Parameters.AddWithValue("country", entity.Country);
                command.Parameters.AddWithValue("isActive", true);

                command.Parameters.AddWithValue("createdByUserId", userId);
                command.Parameters.AddWithValue("updatedByUserId", userId);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteLocationAsync(Guid locationId, Guid id)
        {
            Console.WriteLine(id);
            string selectCommandText = $"SELECT \"IsActive\" FROM \"Location\" WHERE \"Id\" = @id  ";
            string commandText =
                $"UPDATE {TableName} SET \"IsActive\"  = @newIsActive, \"UpdatedByUserId\" = @updatedByUserId WHERE \"Id\" = @id";
            using (var connection = new NpgsqlConnection(_connectionString))
            using (var selectCommand = new NpgsqlCommand(selectCommandText, connection))
            {
                await connection.OpenAsync();
                selectCommand.Parameters.AddWithValue("id", locationId);
                var currentStatus = await selectCommand.ExecuteScalarAsync();
                if (currentStatus == null)
                {
                    throw new Exception("Location not found");
                }

                bool newIsActive = !(bool)currentStatus;

                using (var updateCommand = new NpgsqlCommand(commandText, connection))
                {
                    updateCommand.Parameters.AddWithValue("id", locationId);
                    updateCommand.Parameters.AddWithValue("newIsActive", newIsActive);
                    updateCommand.Parameters.AddWithValue("updatedByUserId", id);

                    updateCommand.ExecuteNonQuery();
                    return await updateCommand.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}