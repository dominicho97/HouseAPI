using MagicHouse_HouseAPI.Models;
using System.Linq.Expressions;

namespace MagicHouse_HouseAPI.Repository.IRepository
{
    public interface IHouseRepository
    {
        Task<List<House>> GetAll(Expression<Func<House, bool>> filter = null);
        Task<House>Get(Expression<Func<House, bool>> filter = null, bool tracked=true);
        Task Create(House entity);
        Task Remove(House entity);

        Task Save();
    }
}
