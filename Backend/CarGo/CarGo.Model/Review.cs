using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Model
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
   

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }
    }

    public class ReviewDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string User { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
