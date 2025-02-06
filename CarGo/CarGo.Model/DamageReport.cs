namespace CarGo.Model
{
    public class DamageReport
    {
        public Guid Id {  get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
        public Guid BookingId { get; set; }
    }
}
