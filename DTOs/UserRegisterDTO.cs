namespace SimpleOLX.DTOs
{
    /// <summary>
    /// Model for register purposes
    /// </summary>
    public class UserRegisterDTO
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
