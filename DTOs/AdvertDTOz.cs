using SimpleOLX.Entities.Enums;

namespace SimpleOLX.DTOs
{
    public class AdvertDTOz
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public float Price { get; set; }
        public string? Image { get; set; }
        public int UserOwnerId { get; set; }
        public float LocalizationLatitude { get; set; }
        public float LocalizationLongitude { get; set; }
    }
}
