using MagicHouse_HouseAPI.Data;
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
            return HouseStore.houseList;
         

        }
    }
}
