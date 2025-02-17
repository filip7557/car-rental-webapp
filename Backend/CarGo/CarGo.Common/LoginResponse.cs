namespace CarGo.Common
{
    public class LoginResponse
    {
        public required Guid UserId { get; set; }
        public required string Token { get; set; }
    }
}
