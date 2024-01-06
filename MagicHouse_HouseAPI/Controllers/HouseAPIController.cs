using MagicHouse_HouseAPI.Models;
using MagicHouse_HouseAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicHouse_HouseAPI.Controllers
{
    [Route("api/HouseAPI")]
    [ApiController]
    public class HouseAPIController:ControllerBase
    {

        [HttpGet]
        public IEnumerable<HouseDTO>GetHouses()
        {
            return new List<HouseDTO>
            {
                new HouseDTO { Id = 1,Name="Pool View"},
                new HouseDTO { Id = 2,Name="Beach View"}
            };

        }
    }
}
