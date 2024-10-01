using WishList.Models;

namespace WishList.Repositories;

public interface IWishRepository
{
	Task<ICollection<Wish>> GetAll();
	Task<Wish> GetById(Guid id);

	Task Add(Wish wish);
	Task Update(Guid id, Wish wish);
	Task Remove(Guid id);
	Task SelectWish(Guid id, Guid userId, Wish model);
	Task<ICollection<Wish>> GetUserWishByUserId(Guid id);
	Task<ICollection<Wish>> GetSelectedWishByUserId(Guid id);

	Guid GetUserIdByName(string name);
}
