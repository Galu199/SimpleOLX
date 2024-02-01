using SimpleOLX.Entities.Enums;

namespace SimpleOLX.DTOs
{
	/// <summary>
	/// Model class for Advert communication
	/// </summary>
	public class AdvertDTO
	{
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
        public string Mail { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public float Price { get; set; }
		public float LocalizationLatitude { get; set; }
		public float LocalizationLongitude { get; set; }
		public AdvertCategoryEnum Category { get; set; }
		public IFormFile? Image { get; set; }
		//Navigation properties
		public int UserOwnerId { get; set; }
	}
}
