using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SimpleOLX.Models
{
	[Table("Users", Schema = "OLX")]
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public DateTime CreationTime { get; set; }

		[JsonIgnore]
		public string PasswordHash { get; set; }

		[JsonIgnore]
		public List<RefreshToken> RefreshTokens { get; set; }

		//FOREIGN KEYS
		[InverseProperty("UserOwner")]
		public virtual ICollection<Advert> AdvertsOwned { get; set; }

		[InverseProperty("UsersWatching")]
		public virtual ICollection<Advert> AdvertsWatched { get; set; }
	}
}
}
