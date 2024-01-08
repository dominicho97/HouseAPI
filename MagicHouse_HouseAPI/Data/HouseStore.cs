using MagicHouse_HouseAPI.Models.DTO;

namespace MagicHouse_HouseAPI.Data
{
    public static class HouseStore
    {
        public static List<HouseDTO> houseList = new List<HouseDTO>
            {
                new HouseDTO { Id = 1,Name="Pool View", Sqft=100, Occupancy=4},
                new HouseDTO { Id = 2,Name="Beach View",Sqft=300, Occupancy=3}
            };
    }
}
