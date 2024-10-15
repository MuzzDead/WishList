using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.DTOs;
using WishList.Services;

namespace WishList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IUserService _userService;
	public AccountController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
	{
		if (!ModelState.IsValid) { return BadRequest(); }

		var result = await _userService.LoginUser(model);
		if (!result.IsSuccess)
		{
			return BadRequest(result.Message);
		}


		return Ok(new { Token = result.Token });
	}


	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
	{
		if (!ModelState.IsValid) { return BadRequest(); }

		var existingUser = await _userService.GetUserByUsername(model.Username);
		if (existingUser != null)
		{
			return BadRequest("Wrong Username or Password!");
		}

		var result = await _userService.RegisterUser(model);
		if (!result.IsSuccess)
		{
			return BadRequest(result.Message);
		}


		return Ok(new { Token = result.Token });
	}


	[HttpDelete]
	public async Task<IActionResult> DeleteUser(Guid userId)
	{
		var user = await _userService.GetUserById(userId);
		if (user == null) { return NotFound(); }

		await _userService.DeleteUser(userId);
		return Ok();
	}

	[HttpGet("userid/{userId:guid}")]
	public async Task<IActionResult> GetUserById(Guid userId)
	{
		var result = await _userService.GetUserById(userId);
		return Ok(result);
	}
}
