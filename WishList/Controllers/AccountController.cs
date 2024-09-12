using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Services;

namespace WishList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly JwtTokenGenerator _jwtTokenGenerator;
	private readonly IUserService _userService;
	public AccountController(JwtTokenGenerator jwtTokenGenerator, IUserService userService)
	{
		_jwtTokenGenerator = jwtTokenGenerator;
		_userService = userService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginModel model)
	{
		if (!ModelState.IsValid) { return BadRequest(); }

		var user = await _userService.GetUserByUsername(model.Username);
		if (user == null)
		{
			return NotFound("User not Found!");
		}

		var passwordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
		if (!passwordValid) { return BadRequest("Passwords don`t match!"); }

		var token = _jwtTokenGenerator.GenerateToken(model.Username);
		return Ok(new { Token = token });
	}


	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterModel model)
	{
		if (!ModelState.IsValid) { return BadRequest(); }

		var existingUser = await _userService.GetUserByUsername(model.Username);
		if (existingUser != null)
		{
			return BadRequest("Wrong Username or Password!");
		}

		var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

		var newUser = new User
		{
			Username = model.Username,
			PasswordHash = hashedPassword
		};

		await _userService.AddUser(newUser);

		var token = _jwtTokenGenerator.GenerateToken(newUser.Username);


		return Ok(new { Token = token });
	}
}
