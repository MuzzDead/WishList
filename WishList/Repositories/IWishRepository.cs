using WishList.Models;

namespace WishList.Repositories;

public interface IWishRepository
{
	Task<ICollection<Wish>> GetAll();
	Task<Wish> GetById(Guid id);

	Task AddWishAsync(Wish wish);
	Task UpdateAsync(Guid id, Wish wish);
	Task RemoveAsync(Guid id);
	Task SelectWish(Guid id, Guid userId);
	Task<ICollection<Wish>> GetUserWishByUserId(Guid id);
	Task<ICollection<Wish>> GetSelectedWishByUserId(Guid id);

	Guid GetUserIdByName(string name);
}
