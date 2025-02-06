using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Common
{
    public class CompanyVehicleFilter
    {
        public bool? IsActive { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? VehicleModelId { get; set; }

        public Guid? ColorId { get; set; }

        //public decimal? MinDailyPrice { get; set; }
        //public decimal? MaxDailyPrice { get; set; }
        public bool? IsOperational { get; set; }
        public Guid? CurrentLocationId { get; set; }
    }
}
