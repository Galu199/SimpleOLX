using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleOLX.DTOs;
using SimpleOLX.Entities;
using SimpleOLX.Entities.Enums;

namespace SimpleOLX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SearchController : ControllerBase
    {
        private readonly SimpleOLXDbContext _context;
        private readonly SignInManager<User> _signInManager;

        public SearchController(SimpleOLXDbContext context, SignInManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

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
