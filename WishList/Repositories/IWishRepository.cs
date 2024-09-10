using WishList.Models;

namespace WishList.Repositories;

public interface IWishRepository
{
	Task<ICollection<Wish>> GetAll();
	Task<Wish> GetById(Guid id);

	Task Add(Wish wish);
	Task Update(Guid id, Wish wish);
	Task Remove(Guid id);
}
