namespace SimpleOLX.Models
{
	public class RefreshToken
	{
		public int Id { get; set; }
		public string Value { get; set; }
		public DateTime ExpiryDate { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? RevocationDate { get; set; }
		public string CreatedByIp { get; set; }
		public string RevokedByIp { get; set; }
		public string ReplacedByToken { get; set; }
		public string ReasonRevoked { get; set; }
		public int UserId { get; set; }

        //Navigation properties
        public User User { get; set; }
	}
}
