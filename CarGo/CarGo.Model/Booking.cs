using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Model
{
    public class Booking
    {
      
        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public Guid UserId { get; set; }

        public Guid CompanyVehicleId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid StatusId { get; set; }

        public Guid PickUpLocationId { get; set; }

        public Guid DropOffLocationId { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }


    }
}
