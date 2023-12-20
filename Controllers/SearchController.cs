using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleOLX.Entities;

namespace SimpleOLX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<Advert>>> GetAdverts(string phraze)
        {
            if (_context.Adverts == null || phraze.IsNullOrEmpty())
            {
                return NotFound(); // bad input
            }
            var searchWords = phraze.Split(" ", StringSplitOptions.RemoveEmptyEntries);
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
    }
}
