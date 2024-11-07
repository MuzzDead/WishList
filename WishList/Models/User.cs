using System.ComponentModel.DataAnnotations;

namespace WishList.Models;

public class User
{
	public Guid Id { get; set; }
	public string Username { get; set; }
	public string PasswordHash { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public string Role { get; set; } = "User";
}

