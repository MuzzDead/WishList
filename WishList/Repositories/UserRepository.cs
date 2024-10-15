using Microsoft.EntityFrameworkCore;
using WishList.ApplicationDbContext;
using WishList.Models;

namespace WishList.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddUserAsync(User user)
		{
			await _appDbContext.Users.AddAsync(user);
			await _appDbContext.SaveChangesAsync();
		}
		
		public async Task<IEnumerable<User>> GetAllUsers()
		{
			return await _appDbContext.Users.AsNoTracking().ToListAsync();
		}

		public async Task<User> GetUserById(Guid userId)
		{
			return await _appDbContext.Users.FindAsync(userId);
		}

		public async Task<User> GetUserByUsername(string userName)
		{
			return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == userName);
		}

		public async Task RemoveUserAsync(Guid userId)
		{
			var user = await GetUserById(userId);

			_appDbContext.Users.Remove(user);
			await _appDbContext.SaveChangesAsync();
		}
	}
}
