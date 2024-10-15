using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WishList.ApplicationDbContext;
using WishList.Models;

namespace WishList.Repositories;

public class WishRepository : IWishRepository
{
	private readonly AppDbContext _context;
	public WishRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task AddWishAsync(Wish wish)
	{
		await _context.Wishes.AddAsync(wish);
		await _context.SaveChangesAsync();
	}

	public async Task<ICollection<Wish>> GetAll()
	{
		return await _context.Wishes.ToListAsync();
	}

	public async Task<Wish> GetById(Guid id)
	{
		return await _context.Wishes.FindAsync(id);
	}

	public async Task<ICollection<Wish>> GetSelectedWishByUserId(Guid id)
	{
		var selectedWishes = await _context.Wishes
			.Where(w => w.SelectedByUserId == id)
			.ToListAsync();

		return selectedWishes;
	}

	public Guid GetUserIdByName(string name)
	{
		var user = _context.Users.FirstOrDefault(u => u.Username == name);
		if (user == null)
		{
			return Guid.Empty;
		}
		return user.Id;
	}

	public async Task<ICollection<Wish>> GetUserWishByUserId(Guid userId)
	{
		var userWishes = await _context.Wishes
			.Where(w => w.UserId == userId)
			.ToListAsync();

		return userWishes;
	}

	public async Task RemoveAsync(Guid id)
	{
		var wish = await GetById(id);
		if (wish != null)
		{
			_context.Wishes.Remove(wish);
			await _context.SaveChangesAsync();
		}
	}

	public async Task SelectWish(Guid id, Guid userId, Wish model)
	{
		var wish = await GetById(id);

		if (wish != null && wish.IsSelected == false)
		{
			wish.IsSelected = true;
			wish.SelectedByUserId = userId;

			await _context.SaveChangesAsync();
		}
	}

	public async Task UpdateAsync(Guid id, Wish wishModel)
	{
		var wish = await GetById(id);
		if (wish != null)
		{
			wish.Description = wishModel.Description;
		}

		await _context.SaveChangesAsync();
	}
}
