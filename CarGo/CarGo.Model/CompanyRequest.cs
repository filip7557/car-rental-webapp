namespace CarGo.Model
{
    public class CompanyRequest
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public Guid UpdatedByUserId { get; set; }
    }
}