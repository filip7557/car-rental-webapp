using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace Repository
{
    public class CarColorRepository : ICarColorRepository
    {
        private string? _connectionString;

        public CarColorRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        //GET ALL
        public async Task<List<CarColor>> GetAllAsync()
        {
            var carColors = new List<CarColor>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\"" +
                                      "FROM \"VehicleColor\"";

                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var carColor = new CarColor()
                            {
                                ID = Guid.Parse(reader[0].ToString()!),
                                Name = reader[1].ToString()!,
                            };

                            carColors.Add(carColor);
                        }
                    }
                    else
                    {
                        connection.Close();
                        return carColors;
                    }

                    connection.Close();

                    return carColors;
                }
            }
            catch (Exception)
            {
                return carColors;
            }
        }

        //GET BY ID
        public async Task<CarColor?> GetByIdAsync(Guid id)
        {
            try
            {
                var carColor = new CarColor() { };

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var commandText = "SELECT " +
                                      "\"Id\", \"Name\"" +
                                      " FROM \"VehicleColor\" " +
                                      "WHERE \"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        carColor.ID = Guid.Parse(reader[0].ToString()!);
                        carColor.Name = reader[1].ToString()!;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return carColor;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}