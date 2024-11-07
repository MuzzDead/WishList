namespace WishList.Models;

public class Wish
{
	public Guid Id { get; set; }
	public string ImageUrl { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public bool IsSelected { get; set; } = false;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public Guid UserId { get; set; }
	public Guid? SelectedByUserId { get; set; }
}
