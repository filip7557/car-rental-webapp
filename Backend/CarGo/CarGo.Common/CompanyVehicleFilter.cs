namespace CarGo.Common
{
    public class CompanyVehicleFilter
    {
        public bool? IsOperational { get; set; }
        public bool? IsActive { get; set; }
        public string? StatusName { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? VehicleModelId { get; set; }
        public Guid? VehicleMakeId { get; set; }
        public Guid? VehicleTypeId { get; set; }
        public Guid? ColorId { get; set; }
        public Guid? CurrentLocationId { get; set; }

        public string? CompanyName { get; set; }
        public string? VehicleModelName { get; set; }
        public string? VehicleMakeName { get; set; }
        public string? ColorName { get; set; }
        public string? LocationAddress { get; set; }
        public string? LocationCity { get; set; }

        public string? PlateNumber { get; set; }
        public string? ImageUrl { get; set; }

        public decimal? MinDailyPrice { get; set; }
        public decimal? MaxDailyPrice { get; set; }

        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }

        public string? UserRole { get; set; }
        public Guid? UserId { get; set; }
    }

}