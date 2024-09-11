using WishList.Models;

namespace WishList.Services;

public interface IUserService
{
	Task<User> GetUserByUsername(string username);
	Task AddUser(User user);
}
