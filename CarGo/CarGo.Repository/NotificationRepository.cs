using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using Npgsql;

namespace CarGo.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private string? _connectionString;

        public NotificationRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
            ?? throw new InvalidOperationException("Database connection string is not set.");
        }

        public async Task<bool> SaveNotificationAsync(Notification notification)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "INSERT INTO \"Notification\" (\"Id\", \"Title\", \"Text\", \"From\", \"To\") VALUES (@id, @title, @text, @from, @to)";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", Guid.NewGuid());
                    command.Parameters.AddWithValue("title", notification.Title);
                    command.Parameters.AddWithValue("text", notification.Text!);
                    command.Parameters.AddWithValue("from", notification.From!);
                    command.Parameters.AddWithValue("to", notification.To!);

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
                Console.WriteLine("Exception:" + ex.Message);
                return false;
            }
        }

        public async Task<List<Notification>> GetAllNotificationsAsync(Paging paging)
        {
            var notifications = new List<Notification>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT \"Id\", \"Title\", \"Text\", \"From\", \"To\" FROM \"Notification\" ORDER BY \"DateCreated\" DESC LIMIT @rpp OFFSET (@pageNumber - 1) * @rpp";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("rpp", paging.Rpp);
                    command.Parameters.AddWithValue("pageNumber", paging.PageNumber);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var notification = new Notification
                            {
                                Id = Guid.Parse(reader[0].ToString()!),
                                Title = reader[1].ToString()!,
                                Text = reader[2].ToString()!,
                                From = reader[3].ToString()!,
                                To = reader[4].ToString()!,
                            };
                            notifications.Add(notification);
                        }
                    }

                    connection.Close();

                    return notifications;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return notifications;
            }
        }

        public async Task<int> CountAsync()
        {
            int count = 0;
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string commandText = "SELECT COUNT(\"Id\") FROM \"Notification\"";
                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        int.TryParse(reader[0].ToString(), out count);
                    }
                    else
                    {
                        connection.Close();
                        return 0;
                    }
                    connection.Close();

                    return count;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
    }
}