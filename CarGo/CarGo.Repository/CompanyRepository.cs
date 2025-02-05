using CarGo.Model;
using CarGo.Repository.Common;
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

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText =
                        "INSERT INTO \"Company\" (\"Id\", \"Name\", \"Email\", \"IsActive\", \"CreatedByUserId\", \"DateCreated\", \"UpdatedByUserId\") " +
                        "VALUES (@id, @name, @email, @isactive, @createdbyuserid, @datecreated, @updatedbyuserid);";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, company.Id);
                        command.Parameters.AddWithValue("name", NpgsqlTypes.NpgsqlDbType.Text, company.Name);
                        command.Parameters.AddWithValue("email", NpgsqlTypes.NpgsqlDbType.Text, company.Email);
                        command.Parameters.AddWithValue("isactive", NpgsqlTypes.NpgsqlDbType.Boolean, company.IsActive);
                        command.Parameters.AddWithValue("createdbyuserid", NpgsqlTypes.NpgsqlDbType.Uuid,
                            company.CreatedByUserId);
                        command.Parameters.AddWithValue("updatedbyuserid", NpgsqlTypes.NpgsqlDbType.Uuid,
                            company.UpdatedByUserId);
                        command.Parameters.AddWithValue("datecreated", company.DateCreated);
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating company: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> NewCompanyLocation(CompanyLocations companyLocations)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "INSERT INTO \"CompanyLocation\" (\"CompanyId\", \"LocationId\") VALUES (@companyId, @locationId);";
                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.CompanyId);
                        command.Parameters.AddWithValue("locationId", NpgsqlTypes.NpgsqlDbType.Uuid, companyLocations.LocationId);
                        await connection.OpenAsync();
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
    }
}