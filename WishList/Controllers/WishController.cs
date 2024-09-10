using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Services;

namespace WishList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishController : ControllerBase
{
	private readonly IWishService _wishService;
	public WishController(IWishService wishService)
	{
		_wishService = wishService;
	}

	[HttpGet]
	public async Task<IActionResult> GetWishes()
	{
		var wishes = await _wishService.GetWishes();
		return Ok(wishes);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetWishById(Guid id)
	{
		var wish = await _wishService.GetWishById(id);
		if (wish == null) return NotFound();

		return Ok(wish);
	}



	[HttpPost]
	public async Task<IActionResult> AddWish([FromBody] Wish model)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);


		await _wishService.AddWish(model);
		return Ok();
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateWish(Guid id, [FromBody] Wish model)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		var existingWish = await _wishService.GetWishById(id);
		if (existingWish == null) return NotFound();


		await _wishService.UpdateWish(id, model);
		return Ok();
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteWish(Guid id)
	{
		var wish = await _wishService.GetWishById(id);
		if (wish == null) return NotFound();


		await _wishService.DeleteWish(id);
		return Ok();
	}
}
