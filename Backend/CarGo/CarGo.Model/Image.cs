using Microsoft.AspNetCore.Http;

namespace CarGo.Model
{
    public class Image
    {
        public Guid Id { get; set; }
        public required string ImageFile { get; set; }
        public Guid DamageReportId { get; set; }
        public bool IsActive { get; set; }
    }

    public class ImageDTO
    {
        public required string ImageFile { get; set; }
        public Guid DamageReportId { get; set; }
    }
}