﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOLX.DTOs;
using SimpleOLX.Entities;

namespace SimpleOLX.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AdvertsController : ControllerBase
	{
		private readonly SimpleOLXDbContext _context;

		public AdvertsController(SimpleOLXDbContext context)
		{
			_context = context;
		}

		// GET: api/Adverts
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Advert>>> GetAdverts()
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
		public async Task<IActionResult> PutAdvert(int id, Advert advert)
		{
			if (id != advert.Id)
			{
				return BadRequest();
			}

			_context.Entry(advert).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AdvertExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Adverts
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Advert>> PostAdvert(AdvertAddDTO advert)
		{
			if (_context.Adverts == null)
			{
				return Problem("Entity set 'SimpleOLXDbContext.Adverts'  is null.");
			}
			var newAdvert = new Advert()
			{
				Title = advert.Title,
				Description = advert.Description,
				Mail = advert.Mail,
				PhoneNumber = advert.PhoneNumber,
				Price = advert.Price,
				LocalizationLatitude = advert.LocalizationLatitude,
				LocalizationLongitude = advert.LocalizationLongitude,
				Category = advert.Category,
				Image = await Helpers.ImageConverter.ConvertIFormFileToByteArray(advert.Image),
				UserOwnerId = advert.UserOwnerId,
				CreationDate = DateTime.Now,
			};
			_context.Adverts.Add(newAdvert);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetAdvert", new { id = newAdvert.Id }, newAdvert);
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

			_context.Adverts.Remove(advert);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool AdvertExists(int id)
		{
			return (_context.Adverts?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
