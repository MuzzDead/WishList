using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WishList.ApplicationDbContext;
using WishList.Models;
using WishList.Services;

namespace WishList.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WishController : ControllerBase
{
	private readonly IWishService _wishService;
	public WishController(IWishService wishService)
	{
		_wishService = wishService;
	}

	[AllowAnonymous]
	[HttpGet]
	public async Task<IActionResult> GetWishes()
	{
		var wishes = await _wishService.GetWishes();
		return Ok(wishes);
	}

	[AllowAnonymous]
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetWishById(Guid id)
	{
		var wish = await _wishService.GetWishById(id);
		if (wish == null) return NotFound();

		return Ok(wish);
	}

	
	[HttpPut("select/{id:guid}")]
	public async Task<IActionResult> SelectWish(Guid id, [FromBody]Wish model)
	{
		var userId = GetUserId();

		var wish = await _wishService.GetWishById(id);

		if (wish == null) return NotFound();
		if (wish.UserId == userId) return BadRequest();

		await _wishService.SelectWish(id, userId, model);
		return Ok();
	}

	[AllowAnonymous]
	[HttpGet("sected-wishes/{userId:guid}")]
	public async Task<IActionResult> GetSelectedWish(Guid userId)
	{
		var selectedWishes = await _wishService.GetSelectedWishes(userId);
		if (selectedWishes == null) return NotFound();

		return Ok(selectedWishes);
	}

	[AllowAnonymous]
	[HttpGet("user-wishes/{userId:guid}")]
	public async Task<IActionResult> GetUserWishes(Guid userId)
	{
		var wishes = await _wishService.GetUserWishes(userId);
		if (wishes == null) return NotFound();

		return Ok(wishes);
	}

	private Guid GetUserId()
	{
		string userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


		var user = _wishService.GetUserIdByName(userIdString);
		if (user == null) return Guid.Empty;

		return user;
	}



	[HttpPost]
	public async Task<IActionResult> CreateWish([FromBody] Wish model)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		var userId = GetUserId();
		if (userId == Guid.Empty)
		{
			return Unauthorized("User not authorized");
		}

		model.UserId = userId;

		await _wishService.AddWish(model);
		return Ok();
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateWish(Guid id, [FromBody] Wish model)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		var existingWish = await _wishService.GetWishById(id);
		if (existingWish == null) return NotFound();

		var userId = GetUserId();

		if (model.UserId != userId)
		{
			return Forbid();
		}

		await _wishService.UpdateWish(id, model);
		return Ok();
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteWish(Guid id)
	{
		var wish = await _wishService.GetWishById(id);
		if (wish == null) return NotFound();

		var userId = GetUserId();

		if (wish.UserId != userId)
		{
			return Forbid();
		}

		await _wishService.DeleteWish(id);
		return Ok();

	}
}
