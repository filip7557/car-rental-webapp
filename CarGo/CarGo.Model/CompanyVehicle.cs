using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Model
{
    public class CompanyVehicle
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid VehicleModelId { get; set; }

        public decimal DailyPrice { get; set; }

        public Guid ColorId { get; set; }

        public string PlateNumber { get; set; }

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
