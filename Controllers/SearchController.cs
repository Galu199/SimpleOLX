using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleOLX.DTOs;
using SimpleOLX.Entities;
using SimpleOLX.Entities.Enums;

namespace SimpleOLX.Controllers
{
    /// <summary>
    /// Searching Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SearchController : ControllerBase
    {
        private readonly SimpleOLXDbContext _context; // Dostęp do bazy dancyh

        public SearchController(SimpleOLXDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Searching for Adverts that contains pharse in title, min letters is 3
        /// </summary>
        /// <param name="phrase">text that is looked for</param>
        /// <returns>Ancestor of List of Adverts (can be converted to List)</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Advert>>> GetAdverts(string phrase)
        {
            if (_context.Adverts == null || phrase.IsNullOrEmpty())
            {
                return NotFound(); // bad input
            }
            var searchWords = phrase.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (!searchWords.Any())
            {
                return NotFound(); // No valid search words
            }
            var adverts = new List<Advert>();
            foreach (var word in searchWords)
            {
                if (word.Length < 3) continue;
                adverts.AddRange(_context.Adverts.Where(i => i.Title.ToLower().Contains(word.ToLower())).ToList());
            }
            if (adverts.IsNullOrEmpty())
            {
                return NotFound(); // No matching adverts found
            }
            return Ok(adverts);
        }

        /// <summary>
        /// Serching for Adverts by the category enumerable value
        /// </summary>
        /// <param name="category">category</param>
        /// <returns>List of Adverts (but converted to DTO for front simplicity)</returns>
        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<AdvertDTOz>>> GetAdvertsByCategory(AdvertCategoryEnum category)
        {
            if (_context.Adverts == null) { return NotFound(); }
            var adverts = await _context.Adverts.Where(a => a.Category == category).Select(a => new AdvertDTOz
            {
                Id = a.Id,
                Description = a.Description,
                Price = a.Price,
                Title = a.Title,
                Image = Helpers.ImageConverter.ConvertByteArrayToBase64String(a.Image),
                LocalizationLatitude = a.LocalizationLatitude,
                LocalizationLongitude = a.LocalizationLongitude,
                UserOwnerId = a.UserOwnerId
            }).ToListAsync();
            return adverts;
        }
    }
}
