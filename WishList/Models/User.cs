using System.ComponentModel.DataAnnotations;

namespace WishList.Models;

public class User
{
	public int Id { get; set; }

	[Required]
	public string Name { get; set; }
	public ICollection<Wish> Wishes { get; set; } = new List<Wish>(0);
}
