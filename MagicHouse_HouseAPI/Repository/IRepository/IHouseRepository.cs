using MagicHouse_HouseAPI.Models;
using System.Linq.Expressions;

namespace MagicHouse_HouseAPI.Repository.IRepository
{
    public interface IHouseRepository
    {
        Task<List<House>> GetAllAsync(Expression<Func<House, bool>> filter = null);
        Task<House> GetAsync(Expression<Func<House, bool>> filter = null, bool tracked=true);
        Task CreateAsync(House entity);

        Task UpdateAsync(House entity);
        Task RemoveAsync(House entity);

        Task SaveAsync();
    }
}
