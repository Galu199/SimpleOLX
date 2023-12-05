using Microsoft.AspNetCore.Identity;

namespace SimpleOLX.Models
{
	public class User : IdentityUser<int>
	{
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime CreationDate { get; set; }

		//Navigation properties
		//public ICollection<RefreshToken> RefreshTokens { get; set; }
		public ICollection<Advert> AdvertsOwned { get; set; }
	}
}
