using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Services;

namespace WishList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly JwtTokenGenerator _jwtTokenGenerator;
	public AccountController(JwtTokenGenerator jwtTokenGenerator)
	{
		_jwtTokenGenerator = jwtTokenGenerator;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginModel model)
	{
		if (model.UserName == "test" && model.Password == "password")
		{
			var token = _jwtTokenGenerator.GenerateToken(model.UserName);
			return Ok(new { Token = token });
		}

		return Unauthorized();
	}
}
