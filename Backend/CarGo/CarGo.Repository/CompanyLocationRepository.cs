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
    public class CompanyLocationRepository: ICompanyLocationRepository
    {
        private string? _connectionString;

        public CompanyLocationRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<bool> AddCompanyLocationAsync(Guid companyId, Guid locationId, Guid userId)
        {
            string commandText =
                "INSERT INTO \"CompanyLocation\" ( \"CompanyId\", \"LocationId\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                "VALUES (@companyId, @locationId, @createdByUserId, @updatedByUserId)";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(commandText, connection))
                {
                 
                    command.Parameters.AddWithValue("companyId", companyId);
                    command.Parameters.AddWithValue("locationId", locationId);
                    command.Parameters.AddWithValue("createdByUserId", userId);
                    command.Parameters.AddWithValue("updatedByUserId", userId);

                    try
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;  
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding company-location association: {ex.Message}");
                        return false;  
                    }
                }
            }
        }



        public async Task<List<Location>> GetLocationByCompanyIdAsync(Guid companyId)
        {
            var locationList = new List<Location>();


            StringBuilder query = new StringBuilder(
                "SELECT l.\"Id\", l.\"Address\", l.\"City\", l.\"Country\" " +
                "FROM \"CompanyLocation\" cl " +
                "INNER JOIN \"Location\" l ON cl.\"LocationId\" = l.\"Id\" " +
                "WHERE cl.\"CompanyId\" = @companyId AND l.\"IsActive\" = true");

            using (var connection = new NpgsqlConnection(_connectionString))
            using (var command = new NpgsqlCommand(query.ToString(), connection))
            {

                command.Parameters.AddWithValue("companyId", companyId);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {

                    var location = new Location
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        City = reader.GetString(reader.GetOrdinal("City")),
                        Country = reader.GetString(reader.GetOrdinal("Country"))
                    };
                    locationList.Add(location);
                }
            }

            return locationList;
        }


        public async Task DeleteCompanyLocationByLocationIdAsync(Guid locationId, Guid updatedByUserId)
        {
            string commandText = @"UPDATE ""CompanyLocation"" SET ""IsActive"" = @isActive, ""UpdatedByUserId"" = @updatedByUserId WHERE ""LocationId"" = @locationId";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(commandText, connection))
                {
                
                    command.Parameters.AddWithValue("@isActive", false); 
                    command.Parameters.AddWithValue("@updatedByUserId", updatedByUserId); 
                    command.Parameters.AddWithValue("@locationId", locationId); 

                    await command.ExecuteNonQueryAsync();
                }
            }
        }






    }

}
