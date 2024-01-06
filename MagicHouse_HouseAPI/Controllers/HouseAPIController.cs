using MagicHouse_HouseAPI.Data;
using MagicHouse_HouseAPI.Models;
using MagicHouse_HouseAPI.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MagicHouse_HouseAPI.Controllers
{
    [Route("api/HouseAPI")]
    [ApiController]
    public class HouseAPIController:ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HouseDTO>>GetHouses()
        {
            return Ok( HouseStore.houseList);
         

        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HouseDTO> GetHouse(int id)
        {
            if(id == 0)
            {
                return BadRequest(); 
            }
            var house = HouseStore.houseList.FirstOrDefault(u => u.Id == id);
            if(house == null)
            {
                return NotFound();
            }

            return Ok(house);


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HouseDTO>CreateHouse(HouseDTO houseDTO) 
        {
            if (houseDTO == null)
            {
                return BadRequest(houseDTO);
            }
            if (houseDTO.Id > 0) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            houseDTO.Id = HouseStore.houseList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            HouseStore.houseList.Add(houseDTO);

            return Ok(houseDTO);
        }
    }
}
