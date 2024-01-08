using MagicHouse_HouseAPI.Data;
using MagicHouse_HouseAPI.Models;
using MagicHouse_HouseAPI.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MagicHouse_HouseAPI.Controllers
{
    [Route("api/HouseAPI")]
    [ApiController]
    public class HouseAPIController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HouseDTO>> GetHouses()
        {
            return Ok(HouseStore.houseList);


        }


        [HttpGet("{id:int}", Name = "GetHouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HouseDTO> GetHouse(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var house = HouseStore.houseList.FirstOrDefault(u => u.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return Ok(house);


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HouseDTO> CreateHouse([FromBody] HouseDTO houseDTO)
        {
            //if(!ModelState.IsValid) 
            //{
            //    return BadRequest(ModelState);
            //}

            if (HouseStore.houseList.FirstOrDefault(u => u.Name.ToLower() == houseDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "House already exists!");
                return BadRequest(ModelState);
            }
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

            return CreatedAtRoute("GetHouse", new { id = houseDTO.Id }, houseDTO);
        }


        [HttpDelete("{id:int}", Name = "DeleteHouse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteHouse(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var house = HouseStore.houseList.FirstOrDefault(u => u.Id == id);
            if (house == null)
            {
                return NotFound();
            }
            HouseStore.houseList.Remove(house);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateHouse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        

        public IActionResult UpdateHouse(int id, [FromBody] HouseDTO houseDTO)
        {
            if (houseDTO == null || id != houseDTO.Id)
            {
                return BadRequest();
            }
            var house = HouseStore.houseList.FirstOrDefault(u => u.Id == id);
            house.Name = houseDTO.Name;
            house.Sqft = houseDTO.Sqft;
            house.Occupancy = houseDTO.Occupancy;

            return NoContent();
        }
       
    

    }



}
