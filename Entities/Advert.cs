using SimpleOLX.Entities.Enums;

namespace SimpleOLX.Entities
{
	/// <summary>
	/// Model for Adverts used in database
	/// </summary>
	public class Advert
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Mail { get; set; }
		public string PhoneNumber { get; set; }
		public float Price { get; set; }
		public float LocalizationLatitude { get; set; }
		public float LocalizationLongitude { get; set; }
		public AdvertCategoryEnum Category { get; set; }
		public byte[] Image { get; set; }
		public DateTime CreationDate { get; set; }

        //Navigation properties
		public int UserOwnerId { get; set; }
        public User UserOwner { get; set; }
	}
}
