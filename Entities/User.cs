using Microsoft.AspNetCore.Identity;

namespace SimpleOLX.Entities
{
	/// <summary>
	/// Model of User used in database
	/// </summary>
	public class User : IdentityUser<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime CreationDate { get; set; }

		//Navigation properties
		public ICollection<Advert> AdvertsOwned { get; set; }
	}
}
