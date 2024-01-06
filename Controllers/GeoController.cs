using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOLX.Entities;

namespace SimpleOLX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeoController : ControllerBase
    {
        private readonly SimpleOLXDbContext _context;

        public GeoController(SimpleOLXDbContext context)
        {
            _context = context;
        }

        [HttpGet("{targetLat}/{targetLon}/{radiusInMeters}")]
        public async Task<ActionResult<List<Advert>>> GetAdvertsWithinRadius(float targetLat, float targetLon, double radiusInMeters)
        {
            if (_context.Adverts == null)
            {
                return NotFound();
            }

            var adverts = await _context.Adverts.ToListAsync();
            var advert = adverts.Where(x => CalculateDistance(x.LocalizationLatitude, x.LocalizationLongitude, targetLat, targetLon) <= radiusInMeters).ToList();

            if (advert == null)
            {
                return NotFound();
            }
            return advert;
        }

        private double CalculateDistance(float lat1, float lon1, float lat2, float lon2)
        {
            const double EarthRadius = 6371000; // Earth's radius in meters

            // Convert degrees to radians
            double latRad1 = lat1 * Math.PI / 180.0;
            double lonRad1 = lon1 * Math.PI / 180.0;
            double latRad2 = lat2 * Math.PI / 180.0;
            double lonRad2 = lon2 * Math.PI / 180.0;

            // Calculate differences
            double dLat = latRad2 - latRad1;
            double dLon = lonRad2 - lonRad1;

            // Calculate distance using Haversine formula
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(latRad1) * Math.Cos(latRad2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = EarthRadius * c;

            return distance;
        }
    }
}
