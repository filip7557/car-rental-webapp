namespace CarGo.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? RoleId { get; set; }
    }

    public class UserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}