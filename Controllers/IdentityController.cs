using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleOLX.Models;
using SimpleOLX.Models.identity;

namespace SimpleOLX.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		private readonly SimpleOLXDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public IdentityController(SimpleOLXDbContext context,
			UserManager<User> userManager, 
			SignInManager<User> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(UserRegister user)
		{
			if (_context.Users == null)
			{
				return Problem("Entity set 'SimpleOLXDbContext.Users'  is null.");
			}

			User newUser = new User()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
			};

			var result = await _userManager.CreateAsync(newUser, user.Password);

			if (result.Succeeded)
			{
				// User created successfully
				await _context.SaveChangesAsync();
				return Ok(newUser);
			}
			else
			{
				// Handle errors (e.g., display error messages)
				var errors = new List<string>();

				foreach (var error in result.Errors)
				{
					errors.Add(error.Description);
				}

				return BadRequest(errors);
			}
			

			//return CreatedAtAction("GetUser", new { id = user.Id }, user);
			
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserLogin user)
		{
			// Assuming user is created and stored in 'user' variable
			User tempUser = new User()
			{
				Email = user.Email
			};

			var signInResult = await _signInManager.PasswordSignInAsync(tempUser, "password123", isPersistent: false, lockoutOnFailure: false);

			if (signInResult.Succeeded)
			{
				// User successfully signed in
				// Redirect or return success response
				return Ok("Very-Special-Token");
			}
			else
			{
				// Login failed
				// Handle the failure (e.g., display error message)
				return BadRequest("Invalid login attempt");
			}
		}

		[HttpGet]
		public async Task<ActionResult<string>> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok("User left the chat");
		}

		//public async Task<ActionResult<string>> GetToken()

	}
}
