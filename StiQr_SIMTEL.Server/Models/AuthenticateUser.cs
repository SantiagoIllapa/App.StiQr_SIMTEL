namespace StiQr_SIMTEL.Server.Models
{
    public class AuthenticateUser
    {
        public string UserName { get; set; } = null!;
        public required string Password { get; set; }
    }
}
