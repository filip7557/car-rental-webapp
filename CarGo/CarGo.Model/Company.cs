using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Model
{
    public class Company
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public class CompanyInfoIdAndNameDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    public class CompanyLocations
    {
        public Guid CompanyId { get; set; }
        //public List<Location?> LocationId  { get; set; }
    }
}