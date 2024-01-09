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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>().HasData(
                new House
                {
                    Id = 1,
                    Name = "Royal House",
                    Details = "details test",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 500,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate=DateTime.Now




                },
                new House
                {
                    Id = 2,
                    Name = "Diamond House",
                    Details = "details test",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 600,
                    Sqft = 600,
                    Amenity = "",
                    CreatedDate = DateTime.Now




                },
                new House
                 {
                    Id = 3,
                    Name = "Emerald House",
                    Details = "details test",
                    ImageUrl = "",
                    Occupancy = 4,
                    Rate = 400,
                    Sqft = 400,
                    Amenity = "",
                    CreatedDate = DateTime.Now




                }



                );

        }
    }
}
