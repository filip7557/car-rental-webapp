namespace CarGo.Model
{
    public class UserCompany
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}