﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Common
{
    public class BookingFilter
    {
        public bool? IsActive { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CompanyVehicleId { get; set; }

    }
}
