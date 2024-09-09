using Microsoft.EntityFrameworkCore;
using WishList.Data;
using WishList.Models;

namespace WishList.Repositories
{
	public class WishRepository : IWishRepository
	{
		private readonly WishDbContext _context;

		public WishRepository(WishDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Wish wish)
		{
			await _context.Wishes.AddAsync(wish);
			await _context.SaveChangesAsync();
		}

		public async Task<ICollection<Wish>> GetAsync()
		{
			return await _context.Wishes.ToListAsync();
		}

		public async Task<Wish> GetById(Guid id)
		{
			return await _context.Wishes.FindAsync(id);
		}

		public async Task RemoveAsync(Guid id)
		{
			var wish = await _context.Wishes.FindAsync(id);
			if (wish != null)
			{
				_context.Wishes.Remove(wish);
				await _context.SaveChangesAsync();
			}
		}

		public async Task UpdateAsync(Guid id, Wish wishModel)
		{
			var wish = await _context.Wishes.FindAsync(id);
			if(wish != null)
			{
				wish.Description = wishModel.Description;
			}
		}
	}
}
