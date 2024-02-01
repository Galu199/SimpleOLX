using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOLX.Entities;

namespace SimpleOLX.Controllers
{
	/// <summary>
	/// Controller for user managment
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly SimpleOLXDbContext _context; // access to database

		public UsersController(SimpleOLXDbContext context)
		{
			_context = context;
		}

        /// <summary>
        /// Get full list of users
        /// </summary>
        /// <returns>list of users, or error code if not found</returns>
        // GET: api/Users
        [HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			if (_context.Users == null)
			{
				return NotFound();
			}

			return await _context.Users.ToListAsync();
		}

        /// <summary>
        /// Get one user by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>User, or error code</returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			if (_context.Users == null)
			{
				return NotFound();
			}

			var user = await _context.Users.FindAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}

		/// <summary>
		/// Edit user to database
		/// In front you should get user first before editig him
		/// </summary>
		/// <param name="id">id</param>
		/// <param name="user">user Object</param>
		/// <returns>Code of action, or error code</returns>
		// PUT: api/Users/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutUser(int id, User user)
		{
			if (id != user.Id)
			{
				return BadRequest();
			}

			_context.Entry(user).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
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

		/// <summary>
		/// Add user to databese
		/// </summary>
		/// <param name="user">User object</param>
		/// <returns>User, or error code</returns>
		// POST: api/Users
		[HttpPost]
		public async Task<ActionResult<User>> PostUser(User user)
		{
			if (_context.Users == null)
			{
				return Problem("Entity set 'SimpleOLXDbContext.Users'  is null.");
			}
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetUser", new { id = user.Id }, user);
		}

		/// <summary>
		/// Deleteing User
		/// </summary>
		/// <param name="id">id</param>
		/// <returns>message Code</returns>
		// DELETE: api/Users/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			if (_context.Users == null)
			{
				return NotFound();
			}
			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		/// <summary>
		/// Check if user exists
		/// </summary>
		/// <param name="id">id of user</param>
		/// <returns>bool value if user is found (true) or not (false)</returns>
		private bool UserExists(int id)
		{
			return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
