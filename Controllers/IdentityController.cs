using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOLX.DTOs;
using SimpleOLX.Entities;
using SimpleOLX.Services;

namespace SimpleOLX.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		private readonly SimpleOLXDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
        private readonly JWTService _JWTService;

        public IdentityController(SimpleOLXDbContext context,
			UserManager<User> userManager, 
			SignInManager<User> signInManager,
			JWTService JWTService)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
            _JWTService = JWTService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
		public async Task<ActionResult> Register(UserRegisterDTO userRegisterDTO)
		{
			if (await _userManager.Users.AnyAsync(user => user.Email == userRegisterDTO.Email.ToLower()))
			{
				return BadRequest("There already exists an account using the provided email.");
			}

			var result = await _userManager.CreateAsync(new User()
            {
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
				UserName = userRegisterDTO.FirstName + userRegisterDTO.LastName,
                Email = userRegisterDTO.Email.ToLower(),
                EmailConfirmed = true,
				CreationDate = DateTime.Now,
            }, userRegisterDTO.Password);

			if (result.Succeeded == false) return BadRequest(result.Errors);

			return Ok("Account created successfully.");
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserLoginDTO userLoginDTO)
		{
			var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
			if (user is null) return BadRequest("Invalid email or password.");
			if (user.EmailConfirmed == false) return Unauthorized("Email is unconfirmed.");

			var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDTO.Password, false);
			if (result.Succeeded == false) return BadRequest("Invalid email or password.");

            return _JWTService.CreateJWT(user);
		}
	}
}
