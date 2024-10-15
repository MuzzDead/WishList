using Microsoft.AspNetCore.Http.HttpResults;
using WishList.Models;
using WishList.Models.DTOs;
using WishList.Repositories;

namespace WishList.Services;

public class WishService : IWishService
{
	private readonly IWishRepository _wishRepository;
	public WishService(IWishRepository wishRepository)
	{
		_wishRepository = wishRepository;
	}

	public async Task AddWish(CreateUpdateWishDTO model)
	{
		var wish = new Wish
		{
			ImageUrl = model.ImageUrl,
			Title = model.Title,
			Description = model.Description,
			UserId = (Guid)model.UserId
		};

		await _wishRepository.AddWishAsync(wish);
	}

	public async Task DeleteWish(Guid id)
	{
		await _wishRepository.RemoveAsync(id);
	}

	public async Task<ICollection<Wish>> GetSelectedWishes(Guid id)
	{
		return await _wishRepository.GetSelectedWishByUserId(id);
	}

	public Guid GetUserIdByName(string name)
	{
		var userId = _wishRepository.GetUserIdByName(name);
		
		return userId;
	}

	public async Task<ICollection<Wish>> GetUserWishes(Guid id)
	{
		return await _wishRepository. GetUserWishByUserId(id);
	}

	public async Task<Wish> GetWishById(Guid id)
	{
		return await _wishRepository.GetById(id);
	}

	public async Task<ICollection<Wish>> GetWishes()
	{
		return await _wishRepository.GetAll();
	}

	public async Task SelectWish(Guid id, Guid userId)
	{
		await _wishRepository.SelectWish(id, userId);
	}

	public async Task UpdateWish(Guid id, CreateUpdateWishDTO model)
	{
		var wish = new Wish
		{
			ImageUrl = model.ImageUrl,
			Title = model.Title,
			Description = model.Description,
			UserId = (Guid)model.UserId
		};

		await _wishRepository.UpdateAsync(id, wish);
	}


}
