using Microsoft.EntityFrameworkCore;
using WishList.ApplicationDbContext;
using WishList.Models;

namespace WishList.Services
{
	public class UserService : IUserService
	{
		private readonly AppDbContext _appDbContext;
		public UserService(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task AddUser(User user)
		{
			_appDbContext.Users.Add(user);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task<User> GetUserByUsername(string username)
		{
			return await _appDbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
		}
	}
}
