using WishList.Models;

namespace WishList.Services;

public interface IWishService
{
	Task<ICollection<Wish>> GetWishes();
	Task<Wish> GetWishById(Guid id);

	Task AddWish(Wish wish);
	Task DeleteWish(Guid id);
	Task UpdateWish(Guid id,Wish wish);
	Task SelectWish(Guid id, Guid userId, Wish model);
	Task<ICollection<Wish>> GetUserWishes(Guid id);
	Task<ICollection<Wish>> GetSelectedWishes(Guid id);

	Guid GetUserIdByName(string name);
}
