using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleOLX.Models
{
	[Table("Adverts", Schema = "OLX")]
	public class Advert
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Mail { get; set; }
		public string PhoneNumber { get; set; }
		public string Category { get; set; }
		public float Price { get; set; }
		public byte[] Image { get; set; }
		public double LocalizationLatitude { get; set; }
		public double LocalizationLongitude { get; set; }
		public double Latitute { get; set; }
		public double Longitute { get; set; }
		public DateTime CreationTime { get; set; }

		//FOREIGN KEYS
		[ForeignKey("UserOwner")]
		public virtual int UserOwnerId { get; set; }
		public virtual User UserOwner { get; set; }

		[InverseProperty("AdvertsWatched")]
		public virtual ICollection<User> UsersWatching { get; set; }
	}
}
