using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Model
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public string BookingStatus { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string CompanyName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
