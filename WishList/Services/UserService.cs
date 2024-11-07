using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using WishList.ApplicationDbContext;
using WishList.Models;
using WishList.Models.DTOs;
using WishList.Repositories;

namespace WishList.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly JwtTokenService _jwtTokenService;
	public UserService(IUserRepository userRepository, JwtTokenService jwtTokenService)
	{
		_userRepository = userRepository;
		_jwtTokenService = jwtTokenService;
	}


	public async Task DeleteUser(Guid userId)
	{
		await _userRepository.RemoveUserAsync(userId);
	}

	public async Task<User> GetUserById(Guid userId)
	{
		return await _userRepository.GetUserById(userId);
	}

	public async Task<User> GetUserByUsername(string username)
	{
		return await _userRepository.GetUserByUsername(username);
	}

	public async Task<(bool IsSuccess, string Message, string Token)> RegisterUser(RegisterUserDTO model)
	{
		var existedUser = await _userRepository.GetUserByUsername(model.Username);
		if (existedUser != null) { return (false, "User with this Username already exists", null); }

		var user = new User
		{
			Username = model.Username,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
			Role = "User"
		};

		await _userRepository.AddUserAsync(user);


		var jwtToken = _jwtTokenService.GenerateToken(user.Id, user.Username);

		return (true, "Success!", jwtToken);
	}

	public async Task<(bool IsSuccess, string Message, string Token)> LoginUser(LoginUserDTO model)
	{
		var user = await _userRepository.GetUserByUsername(model.Username);
		if (user == null) { return (false, "User not Found!", null); }

		var IsPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
		if (!IsPasswordValid) { return (false, "Invalid password!", null); }


		var jwtToken = _jwtTokenService.GenerateToken(user.Id, user.Username);

		return (true, "Success!", jwtToken);
	}

	public async Task<IEnumerable<UserDTO>> SearchUsers(string searchString)
	{
		var users = await _userRepository.SearchUsersAsync(searchString);

		return users.Select(user => new UserDTO
		{
			Id = user.Id,
			Username = user.Username,
			CreatedAt = user.CreatedAt
		});
	}
}
