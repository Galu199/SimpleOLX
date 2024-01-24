using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOLX.DTOs;
using SimpleOLX.Entities;
using SimpleOLX.Entities.Enums;
using System.Security.Claims;

namespace SimpleOLX.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AdvertsController : ControllerBase
	{
		private readonly SimpleOLXDbContext _context;
        private readonly SignInManager<User> _signInManager;

        public AdvertsController(SimpleOLXDbContext context, SignInManager<User> signInManager)
		{
			_context = context;
			_signInManager = signInManager;
		}

		// GET: api/Adverts
		[HttpGet]
		public async Task<ActionResult<List<Advert>>> GetAdverts()
		{
			if (_context.Adverts == null)
			{
				return NotFound();
			}
			return await _context.Adverts.ToListAsync();
		}

		// GET: api/Adverts/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Advert>> GetAdvert(int id)
		{
			if (_context.Adverts == null)
			{
				return NotFound();
			}
			var advert = await _context.Adverts.FindAsync(id);

			if (advert == null)
			{
				return NotFound();
			}

			return advert;
		}

		// PUT: api/Adverts/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAdvert(int id, AdvertDTO advertDTO)
		{
            if (_context.Adverts == null)
            {
                return NotFound();
            }
            var advert = await _context.Adverts.FindAsync(id);
            if (advert == null) return NotFound();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
			if (advert.UserOwnerId.ToString() != userId) return Unauthorized("No access to edit this post");

            MapAdvertDTOToAdvert(advertDTO, advert);

            try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException a)
			{
				if (!AdvertExists(id))
				{
					return NotFound();
                }
                return BadRequest(a.Message);
            }
			return NoContent();
		}

		// POST: api/Adverts
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Advert>> PostAdvert(AdvertDTO advertDTO)
		{
			if (_context.Adverts == null)
			{
				return Problem("Entity set 'SimpleOLXDbContext.Adverts' is null.");
			}

            var advert = new Advert()
            {
                Title = advertDTO.Title,
                Description = advertDTO.Description,
                Mail = advertDTO.Mail,
                PhoneNumber = advertDTO.PhoneNumber,
                Price = advertDTO.Price,
                LocalizationLatitude = advertDTO.LocalizationLatitude,
                LocalizationLongitude = advertDTO.LocalizationLongitude,
                Category = advertDTO.Category,
                Image = await Helpers.ImageConverter.ConvertIFormFileToByteArray(advertDTO.Image),
                UserOwnerId = advertDTO.UserOwnerId,
                CreationDate = DateTime.Now,
            };
            _context.Adverts.Add(advert);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException a)
            {
                return BadRequest(a.Message);
            }

            return CreatedAtAction("GetAdvert", new { id = advert.Id }, advert);
		}

		// DELETE: api/Adverts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAdvert(int id)
		{
			if (_context.Adverts == null)
			{
				return NotFound();
			}
			var advert = await _context.Adverts.FindAsync(id);
			if (advert == null)
			{
				return NotFound();
			}
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
			if (advert.UserOwnerId.ToString() != userId)
			{
				return Unauthorized("No access to delete this post");
			}

            _context.Adverts.Remove(advert);
			await _context.SaveChangesAsync();

			return Ok();
		}

		[HttpGet("category/{category}")]
		[AllowAnonymous]
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

		private bool AdvertExists(int id)
		{
			return (_context.Adverts?.Any(e => e.Id == id)).GetValueOrDefault();
		}

        private void MapAdvertDTOToAdvert(AdvertDTO source, Advert destination)
        {
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.Mail = source.Mail;
            destination.PhoneNumber = source.PhoneNumber;
            destination.Price = source.Price;
            destination.LocalizationLatitude = source.LocalizationLatitude;
            destination.LocalizationLongitude = source.LocalizationLongitude;
			destination.Category = source.Category;
			if(source.Image!=null) destination.Image = Helpers.ImageConverter.ConvertIFormFileToByteArray(source.Image).Result;
        }
    }
}
