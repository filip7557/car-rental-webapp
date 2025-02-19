namespace CarGo.Model
{
    public class CompanyVehicleDTO
    {
        public Guid CompanyVehicleId { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMake { get; set; }
        public string CompanyName { get; set; }
        public Guid CompanyId { get; set; }
        public string VehicleType { get; set; }
        public decimal? DailyPrice { get; set; }
        public string? PlateNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Color { get; set; }
        public Guid? ColorId { get; set; }
        public int? EnginePower { get; set; }
        public bool isActive { get; set; } 

    }
        
    public class CompanyVehicle
    {
        public Guid? Id { get; set; }

        public Guid? CompanyId { get; set; }

        public Guid VehicleModelId { get; set; }

        public decimal DailyPrice { get; set; }

        public Guid ColorId { get; set; }

        public required string PlateNumber { get; set; }

        public string? ImageUrl { get; set; }

        public Guid? CurrentLocationId { get; set; }

        public bool IsOperational { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }

    }
}