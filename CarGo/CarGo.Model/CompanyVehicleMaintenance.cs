using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Model
{
    public class CompanyVehicleMaintenance
    {
        public Guid Id { get; set; }
        public Guid CompanyVehicleId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
