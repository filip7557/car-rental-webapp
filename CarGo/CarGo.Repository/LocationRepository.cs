using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;
using CarGo.Repository.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CarGo.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private string connectionString; 
        protected readonly string TableName = "\"Location\"";

        public LocationRepository(IConfiguration configuration)
        {
            connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb");
        }

        public async Task<List<Location>> GetLocationAsync()
        {

            var locationList = new List<Location>();
            StringBuilder query = new StringBuilder($"SELECT \"Id\", \"Address\", \"City\", \"Country\" FROM \"Location\" Where \"IsActive\" = true");
            using (var connection = new NpgsqlConnection(connectionString))
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
            string commandText = $"SELECT \"Id\", \"Address\", \"City\", \"Country\" FROM \"Location\" WHERE \"Id\" = @id ";
            using (var connection = new NpgsqlConnection(connectionString)) 
            using(var command = new NpgsqlCommand(commandText, connection))
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

        public async Task<bool> PostLocationAsync(Location entity, Guid id) {
            string commandText = $"INSERT INTO {TableName} (\"Id\", \"Address\", \"City\", \"Country\", \"IsActive\",\"CreatedByUserId\", \"UpdatedByUserId\") VALUES (@id, @address, @city, @country, @isActive,@createdByUserId, @UpdatedByUserId)";
            using (var connection = new NpgsqlConnection(connectionString))
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                await connection.OpenAsync();
                command.Parameters.AddWithValue("id", Guid.NewGuid());
                command.Parameters.AddWithValue("address", entity.Address);
                command.Parameters.AddWithValue("city", entity.City);
                command.Parameters.AddWithValue("country", entity.Country);
                command.Parameters.AddWithValue("isActive", true);
                
                command.Parameters.AddWithValue("createdByUserId", id);
                command.Parameters.AddWithValue("updatedByUserId", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteLocationAsync(Guid id)
        {
            string selectCommandText = $"SELECT \"IsActive\" FROM \"Location\" WHERE \"Id\" = @id  ";
            string commandText = $"UPDATE {TableName} SET \"IsActive\"  = @newIsActive WHERE \"Id\" = @id";
            using (var connection = new NpgsqlConnection(connectionString))
            using (var selectCommand = new NpgsqlCommand(selectCommandText, connection))
            {
                await connection.OpenAsync();
                selectCommand.Parameters.AddWithValue("id", id);
                var currentStatus = await selectCommand.ExecuteScalarAsync();
                if (currentStatus == null)
                {
                    throw new Exception("Location not found");
                }
                bool newIsActive = !(bool)currentStatus;


                using (var updateCommand = new NpgsqlCommand(commandText, connection))
                {
                    updateCommand.Parameters.AddWithValue("id", id);
                    updateCommand.Parameters.AddWithValue("newIsActive", newIsActive);
                    updateCommand.ExecuteNonQuery();
                    return await updateCommand.ExecuteNonQueryAsync() > 0;
                }


            }
        }

        
    }
}
