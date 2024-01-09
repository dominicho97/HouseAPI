using MagicHouse_HouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicHouse_HouseAPI.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }
        public DbSet<House> houses {  get; set; }
    }
}
