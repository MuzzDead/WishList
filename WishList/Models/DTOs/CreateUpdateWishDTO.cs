namespace WishList.Models.DTOs;

public class CreateUpdateWishDTO
{
		public string ImageUrl { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Guid? UserId { get; set; }
}
