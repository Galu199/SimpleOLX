namespace SimpleOLX.DTOs
{
    /// <summary>
    /// Model for Login purposes
    /// </summary>
    public class UserLoginDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
