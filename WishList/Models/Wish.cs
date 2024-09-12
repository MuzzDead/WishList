namespace WishList.Models;

public class Wish
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public bool IsSelected { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	// public Guid UserId { get; set; }
	// public User User { get; set; }
}
