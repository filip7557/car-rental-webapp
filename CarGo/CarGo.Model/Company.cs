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
        public required Guid CompanyId { get; set; }
        public required Guid LocationId { get; set; }

        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }

        public DateTime DateUpdated { get; set; }
    }

    public class CompanyLocationsDto
    {
        public required Guid CompanyId { get; set; }
        public required Guid LocationId { get; set; }
        public bool IsActive { get; set; }
    }
}