using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public const string tableName = "\"Company\"";

        //private string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb");
        private string connectionString = "Host=localhost:5432;" + "Username=postgres;" +
            "Password=12345;" +
            "Database=CarGo";

        public async Task<CompanyInfoDto?> GetCompanyAsync(Guid id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT \"Company\".\"Id\", \"Company\".\"Name\", \"Company\".\"Email\", \"Location\".\"Address\", \"Location\".\"City\", \"Location\".\"Country\" " +
                                      "FROM \"Company\" " +
                                      "INNER JOIN \"CompanyLocation\" ON \"Company\".\"Id\" = \"CompanyLocation\".\"CompanyId\" " +
                                      "INNER JOIN \"Location\" ON \"CompanyLocation\".\"LocationId\" = \"Location\".\"Id\" " +
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
    }
}