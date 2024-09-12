using System.ComponentModel.DataAnnotations;

namespace WishList.Models;

public class User
{
	public Guid Id { get; set; }

	[Required]
	public string Username { get; set; }

	[Required]
	public string PasswordHash { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public ICollection<Wish> Wishes { get; set; } = new List<Wish>(0);

	public string Role { get; set; } = "User";
}
