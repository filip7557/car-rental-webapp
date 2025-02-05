namespace CarGo.Model
{
    public class CompanyRequest
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required bool IsActive { get; set; }
        public required bool IsApproved { get; set; }
    }
}