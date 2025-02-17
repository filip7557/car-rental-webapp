using CarGo.Model;
using Npgsql;

namespace CarGo.Repository
{
    public class ImageRepository : IImageRepository
    {
        private string? _connectionString;

        public ImageRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
                                ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<bool> SaveImageAsync(Image image, Guid createdByUserId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText =
                        $"INSERT INTO \"Image\" (\"Id\", \"ImageFile\", \"DamageReportId\", \"CreatedByUserId\", \"UpdatedByUserId\") VALUES (@id, @file, @damageReportId, @createdBy, @updatedBy)";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                    command.Parameters.AddWithValue("file", image.ImageFile);
                    command.Parameters.AddWithValue("damageReportId", image.DamageReportId);
                    command.Parameters.AddWithValue("createdBy", createdByUserId);
                    command.Parameters.AddWithValue("updatedBy", createdByUserId);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
                    {
                        connection.Close();
                        return false;
                    }

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Guid>> GetImageIdsByDamageReportIdAsync(Guid damageReportId)
        {
            var imageIds = new List<Guid>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT \"Id\" FROM \"Image\" WHERE \"DamageReportId\" = @damageReportId;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("damageReportId", NpgsqlTypes.NpgsqlDbType.Uuid, damageReportId);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            imageIds.Add(Guid.Parse(reader[0].ToString() ?? Guid.Empty.ToString()));
                        }
                    }

                    connection.Close();
                    return imageIds;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return imageIds;
            }
        }

        public async Task<Image?> GetImageByIdAsync(Guid id)
        {
            Image? image = null;
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT \"Id\", \"ImageFile\" FROM \"Image\" WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            image = new Image
                            {
                                Id = Guid.Parse(reader[0].ToString()!),
                                ImageFile = (byte[])reader[1],
                            };
                        }
                    }

                    connection.Close();
                    return image;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return image;
            }
        }

        public async Task<bool> DeleteImageByIdAsync(Guid imageId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "DELETE FROM \"Image\" WHERE \"Id\" = @id";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", imageId);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
                    {
                        connection.Close();
                        return false;
                    }

                    connection.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }
    }
}