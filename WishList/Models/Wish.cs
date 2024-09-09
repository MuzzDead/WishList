namespace WishList.Models;

public class Wish
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public bool IsSelected { get; set; }
}
