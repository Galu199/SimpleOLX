namespace SimpleOLX.Models
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public DateTime CreationDate { get; set; }
		public string PasswordHash { get; set; }

		//Navigation properties
		public ICollection<RefreshToken> RefreshTokens { get; set; }
		public ICollection<Advert> AdvertsOwned { get; set; }
		public ICollection<Advert> AdvertsWatched { get; set; }
	}
}
