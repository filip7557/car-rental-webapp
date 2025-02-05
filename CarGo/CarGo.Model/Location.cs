namespace CarGo.Model
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; } = DateTime.Now;
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
    }
}