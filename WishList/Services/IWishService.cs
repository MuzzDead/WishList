using WishList.Models;
using WishList.Models.DTOs;

namespace WishList.Services;

public interface IWishService
{
	Task<ICollection<Wish>> GetWishes();
	Task<Wish> GetWishById(Guid id);

	Task AddWish(CreateUpdateWishDTO model);
	Task DeleteWish(Guid id);
	Task UpdateWish(Guid id, CreateUpdateWishDTO model);
	Task SelectWish(Guid id, Guid userId);
	Task DeselectWish(Guid id, Guid userId);
	Task<ICollection<Wish>> GetUserWishes(Guid id);
	Task<ICollection<Wish>> GetSelectedWishes(Guid id);

	Guid GetUserIdByName(string username);
}
