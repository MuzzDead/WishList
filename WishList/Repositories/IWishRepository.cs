using WishList.Models;

namespace WishList.Repositories;

public interface IWishRepository
{
	Task<ICollection<Wish>> GetAsync();
	Task<Wish> GetById(Guid id);

	Task AddAsync(Wish wish);
	Task UpdateAsync(Guid id, Wish wishModel);
	Task RemoveAsync(Guid id);

}
