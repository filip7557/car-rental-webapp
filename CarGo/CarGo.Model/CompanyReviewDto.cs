using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Model
{
    public class CompanyInfoDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required List<string> Locations { get; set; }

        //Add review list when Review model is done
        //public List<Review> Reviews { get; set; }
    }
}