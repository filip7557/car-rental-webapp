namespace CarGo.Model
{
    public class Notification
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Text { get; set; }
        public required Guid BookingId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
    }
}
