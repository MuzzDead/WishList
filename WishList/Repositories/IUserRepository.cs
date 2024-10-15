using WishList.Models;

namespace WishList.Repositories
{
	public interface IUserRepository
	{
		Task AddUserAsync(User user);
		Task RemoveUserAsync(Guid userId);
		Task<User> GetUserById(Guid userId);
		Task<IEnumerable<User>> GetAllUsers();
		Task<User> GetUserByUsername(string userName);
	}
}
