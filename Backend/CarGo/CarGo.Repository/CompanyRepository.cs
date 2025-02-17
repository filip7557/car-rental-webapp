using CarGo.Model;
using CarGo.Repository.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace CarGo.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public const string tableName = "\"Company\"";

        private string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb");

        public async Task<List<CompanyInfoIdAndNameDto>> GetCompaniesAsync()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\" FROM \"Company\";";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        await connection.OpenAsync();
                        await using var reader = await command.ExecuteReaderAsync();
                        var companies = new List<CompanyInfoIdAndNameDto>();
                        while (await reader.ReadAsync())
                        {
                            companies.Add(new CompanyInfoIdAndNameDto
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!
                            });
                        }

                        return companies;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching companies: {ex.Message}");
                return new List<CompanyInfoIdAndNameDto>();
            }
        }

        public async Task<CompanyInfoDto?> GetCompanyAsync(Guid id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText =
                        "SELECT \"Company\".\"Id\", \"Company\".\"Name\", \"Company\".\"Email\", \"Location\".\"Address\", \"Location\".\"City\", \"Location\".\"Country\" " +
                        "FROM \"Company\" " +
                        "LEFT JOIN \"CompanyLocation\" ON \"Company\".\"Id\" = \"CompanyLocation\".\"CompanyId\" " +
                        "LEFT JOIN \"Location\" ON \"CompanyLocation\".\"LocationId\" = \"Location\".\"Id\" " +
                        "WHERE \"Company\".\"Id\" = @id;";

                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                        await connection.OpenAsync();
                        await using var reader = await command.ExecuteReaderAsync();

                        CompanyInfoDto? companyInfo = null;

                        while (await reader.ReadAsync())
                        {
                            if (companyInfo == null)
                            {
                                companyInfo = new CompanyInfoDto
                                {
                                    Id = Guid.Parse(reader["Id"].ToString()!),
                                    Name = reader["Name"].ToString()!,
                                    Email = reader["Email"].ToString()!,
                                    Locations = new List<string>()
                                };
                            }

                            var location = $"{reader["Address"]}, {reader["City"]}, {reader["Country"]}";
                            companyInfo.Locations.Add(location);
                        }

                        return companyInfo;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching company data: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> NewCompanyLocationAsync(CompanyLocations companyLocations)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "INSERT INTO \"CompanyLocation\" (\"CompanyId\", \"LocationId\", \"IsActive\", \"CreatedByUserId\", \"DateCreated\",\"DateUpdated\", \"UpdatedByUserId\") " +
                        "VALUES (@companyId, @locationId, @isactive, @createdbyuserid, @datecreated, @dateupdated, @updatedbyuserid);";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.CompanyId);
                        command.Parameters.AddWithValue("locationId", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.LocationId);
                        command.Parameters.AddWithValue("isactive", NpgsqlTypes.NpgsqlDbType.Boolean, companyLocations.IsActive);
                        command.Parameters.AddWithValue("createdbyuserid", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.CreatedByUserId);
                        command.Parameters.AddWithValue("updatedbyuserid", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.UpdatedByUserId);
                        command.Parameters.AddWithValue("dateupdated", companyLocations.DateUpdated);
                        command.Parameters.AddWithValue("datecreated", companyLocations.DateCreated);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating company location: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCompanyLocationAsync(CompanyLocations companyLocations)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "DELETE FROM \"CompanyLocation\" WHERE \"CompanyId\" = @companyId AND \"LocationId\" = @locationId;";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.CompanyId);
                        command.Parameters.AddWithValue("locationId", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.LocationId);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting company location: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCompanyLocationAsync(CompanyLocations companyLocations)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "UPDATE \"CompanyLocation\" SET \"IsActive\" = @isactive, \"UpdatedByUserId\" = @updatedbyuserid, \"DateUpdated\" = @dateupdated " +
                                      "WHERE \"CompanyId\" = @companyId AND \"LocationId\" = @locationId;";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.CompanyId);
                        command.Parameters.AddWithValue("locationId", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.LocationId);
                        command.Parameters.AddWithValue("isactive", companyLocations.IsActive);
                        command.Parameters.AddWithValue("updatedbyuserid", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.UpdatedByUserId);
                        command.Parameters.AddWithValue("dateupdated", companyLocations.DateUpdated);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating company location: {ex.Message}");
                return false;
            }
        }

        public async Task<List<CompanyLocationsDto>> GetAllCompanyLocationsAsync()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT \"CompanyId\", \"LocationId\", \"IsActive\" FROM \"CompanyLocation\";";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        connection.Open();
                        await using var reader = await command.ExecuteReaderAsync();
                        var companyLocations = new List<CompanyLocationsDto>();
                        while (await reader.ReadAsync())
                        {
                            companyLocations.Add(new CompanyLocationsDto
                            {
                                CompanyId = Guid.Parse(reader["CompanyId"].ToString()!),
                                LocationId = Guid.Parse(reader["LocationId"].ToString()!),
                                IsActive = bool.Parse(reader["IsActive"].ToString()!)
                            });
                        }
                        return companyLocations;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching company locations: {ex.Message}");
                return new List<CompanyLocationsDto>();
            }
        }

        public async Task<bool> ChangeCompanyIsActiveStatusAsync(Guid Id, bool isActive, Guid UpdatedByUserId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "UPDATE \"Company\" SET \"IsActive\" = @isactive, \"UpdatedByUserId\" = @updatedByUserId WHERE \"Id\" = @id;";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, Id);
                        command.Parameters.AddWithValue("isactive", isActive);
                        command.Parameters.AddWithValue("updatedByUserId", NpgsqlTypes.NpgsqlDbType.Uuid, UpdatedByUserId);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing company status: {ex.Message}");
                return false;
            }
        }
    }
}