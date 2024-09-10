using WishList.Models;

namespace WishList.Services;

public interface IWishService
{
	Task<ICollection<Wish>> GetWishes();
	Task<Wish> GetWishById(Guid id);

	Task AddWish(Wish wish);
	Task DeleteWish(Guid id);
	Task UpdateWish(Guid id,Wish wish);
}
