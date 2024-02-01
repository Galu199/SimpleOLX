using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOLX.DTOs;
using SimpleOLX.Entities;
using System.Security.Claims;

namespace SimpleOLX.Controllers
{
    /// <summary>
    /// Kontroller Ogłoszeń
    /// Tutaj są dokonywane działania na ogłoszeniach
    /// Ogólny dostęp do wszystkich metod jest objęty autoryzacją z wyjątkiem wyznaczonych metod
    /// </summary>
    [Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AdvertsController : ControllerBase
	{
		private readonly SimpleOLXDbContext _context;//Pozwala na dostęp do bazy danych

		//Konstruktor
        public AdvertsController(SimpleOLXDbContext context)
		{
			_context = context;
		}

        /// <summary>
        /// Pobieranie wszystkich ogłoszeń
        /// </summary>
        /// <returns>List of Adverts, or error code</returns>
        // GET: api/Adverts
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Advert>>> GetAdverts()
		{
			if (_context.Adverts == null)
			{
				return NotFound();
			}
			return await _context.Adverts.ToListAsync();
		}

        /// <summary>
        /// Wyszukanie ogłoszenia po ID
        /// </summary>
        /// <param name="id"> id </param>
        /// <returns> Advert, or error code </returns>
        // GET: api/Adverts/5
        [HttpGet("{id}")]
		[AllowAnonymous]
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

		/// <summary>
		/// Edytowanie ogłoszenia po ID
		/// </summary>
		/// <param name="id">id</param>
		/// <param name="advertDTO">object of advertDTO</param>
		/// <returns> message code </returns>
		// PUT: api/Adverts/5
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

        /// <summary>
        /// Dodawanie ogłoszenia
        /// </summary>
        /// <param name="advertDTO">advertDTO object</param>
        /// <returns> Advert, or error code </returns>
        // POST: api/Adverts
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

		/// <summary>
		/// Usuwanie ogłoszenia
		/// </summary>
		/// <param name="id">id</param>
		/// <returns>message code</returns>
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

		/// <summary>
		/// Sprawdza czy ogłoszenie istnieje
		/// </summary>
		/// <param name="id"> id </param>
		/// <returns> true if advert exists, false otherwise </returns>
		private bool AdvertExists(int id)
		{
			return (_context.Adverts?.Any(e => e.Id == id)).GetValueOrDefault();
		}

		/// <summary>
		/// Mapowanie AdcertDTO do Advert
		/// </summary>
		/// <param name="source">advert DTO as source</param>
		/// <param name="destination">Advert to put the data</param>
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
