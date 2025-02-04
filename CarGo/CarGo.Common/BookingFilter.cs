namespace CarGo.Common
{
    public class BookingFilter
    {
        public bool? IsActive { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CompanyVehicleId { get; set; }
    }
}