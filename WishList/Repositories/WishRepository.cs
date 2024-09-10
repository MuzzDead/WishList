using Microsoft.EntityFrameworkCore;
using WishList.ApplicationDbContext;
using WishList.Models;

namespace WishList.Repositories;

public class WishRepository : IWishRepository
{
	private readonly WishDbContext _context;
	public WishRepository(WishDbContext context)
	{
		_context = context;
	}

	public async Task Add(Wish wish)
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

	public async Task Remove(Guid id)
	{
		var wish = await GetById(id);
		if (wish != null)
		{
			_context.Wishes.Remove(wish);
			await _context.SaveChangesAsync();
		}
	}

	public async Task Update(Guid id, Wish wishModel)
	{
		var wish = await GetById(id);
		if (wish != null)
		{
			wish.Description = wishModel.Description;
		}

		await _context.SaveChangesAsync();
	}
}
