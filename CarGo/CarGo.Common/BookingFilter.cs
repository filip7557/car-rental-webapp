namespace CarGo.Common
{
    public class BookingFilter
    {
        public bool? IsActive { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CompanyVehicleId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal? TotalPrice { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? PickUpLocationId { get; set; }

        public Guid? DropOffLocationId { get; set; }

        public string? BookingStatusName { get; set; }
        public string? LocationAddress { get; set; }
        public string? VehicleMakeName { get; set; }
        public string? VehicleModelName { get; set; }
        public string? ImageUrl { get; set; }
    }
}