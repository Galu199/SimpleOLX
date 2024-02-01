using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOLX.Entities;

namespace SimpleOLX.Controllers
{
    /// <summary>
    /// Kontroler do operacji na geolokacjach ogłoszeń
    /// Korzystanie z tego kontrollera wymaga logowania
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeoController : ControllerBase
    {
        private readonly SimpleOLXDbContext _context;//Dostęp do bazy danych

        //Konstruktor
        public GeoController(SimpleOLXDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Metoda zwraca listę ogłoszeń na podstawie geolokacji i promieniu wyszukiwanych ogłoszeń
        /// </summary>
        /// <param name="targetLat">Latitude of start point</param>
        /// <param name="targetLon">Longitude of start point</param>
        /// <param name="radiusInMeters"> radius of serching circle in meters </param>
        /// <returns> List of Adverts, or error code </returns>
        [HttpGet("{targetLat}/{targetLon}/{radiusInMeters}")]
        public async Task<ActionResult<List<Advert>>> GetAdvertsWithinRadius(float targetLat, float targetLon, double radiusInMeters)
        {
            if (_context.Adverts == null)
            {
                return NotFound();
            }

            // Spatial - do sql geo przestrzenne
            // Mój komentarz został usunięty ale dopiszę jeszcze raz
            // Dziękuję za poradę ale musiał bym przebudować na nowo model klasy oraz bazę danych.
            var adverts = await _context.Adverts.ToListAsync();
            var advert = adverts.Where(x => CalculateDistance(x.LocalizationLatitude, x.LocalizationLongitude, targetLat, targetLon) <= radiusInMeters).ToList();

            if (advert == null)
            {
                return NotFound();
            }

            return advert;
        }

        /// <summary>
        /// Metoda wyszukiwania (stack overflow)
        /// w skrócie
        /// zamieniamy jednostki dwóch lokalizacji
        /// sprawdza odległość pomiędzy tymi lokalizacjami
        /// sprawdzamy odległość biorąc pod uwagę że ziemia jest okrągła (Haversine formula)
        /// i na koniec zamieniamy wynik na metry na ziemi
        /// zwracamy odległość
        /// </summary>
        /// <param name="lat1">latitude of point 1</param>
        /// <param name="lon1">longitude of point 1</param>
        /// <param name="lat2">latitude of point 2</param>
        /// <param name="lon2">longitude of point 2</param>
        /// <returns> distance in meters </returns>
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
