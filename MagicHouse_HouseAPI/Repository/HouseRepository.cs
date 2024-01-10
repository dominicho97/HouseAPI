using MagicHouse_HouseAPI.Data;
using MagicHouse_HouseAPI.Models;
using MagicHouse_HouseAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicHouse_HouseAPI.Repository
{
    public class HouseRepository : IHouseRepository
    {
        private readonly ApplicationDbContext _db;

        public HouseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(House entity)
        {
            await _db.Houses.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<House> GetAsync(Expression<Func<House, bool>> filter = null, bool tracked = true)
        {
            {
                IQueryable<House> query = _db.Houses;
                if(!tracked)
                {
                    query = query.AsNoTracking();
                }


                if (filter != null)
                {
                    query.Where(filter);
                }
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<List<House>> GetAllAsync(Expression<Func<House, bool>> filter = null)
        {
            IQueryable<House> query = _db.Houses;
            if(filter != null)
            {
                query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(House entity)
        {
             _db.Houses.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(House entity)
        {
            _db.Houses.Update(entity);
            await SaveAsync();
        }
    }
}
