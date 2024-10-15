using WishList.Models;
using WishList.Models.DTOs;

namespace WishList.Services;

public interface IUserService
{
	Task<User> GetUserByUsername(string username);
	Task<User> GetUserById(Guid userId);
	Task DeleteUser(Guid userId);

	Task<(bool IsSuccess, string Message, string Token)> RegisterUser(RegisterUserDTO model);
	Task<(bool IsSuccess, string Message, string Token)> LoginUser(LoginUserDTO model);
}
