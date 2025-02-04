namespace CarGo.Model
{
    public class Notification
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Text { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
    }
}
