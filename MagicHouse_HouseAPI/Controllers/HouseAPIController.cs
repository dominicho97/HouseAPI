using AutoMapper;
using MagicHouse_HouseAPI.Data;

using MagicHouse_HouseAPI.Models;
using MagicHouse_HouseAPI.Models.DTO;
using MagicHouse_HouseAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MagicHouse_HouseAPI.Controllers
{
    //localhost:7027/api/houseapi
    [Route("api/HouseAPI")]
    [ApiController]
    public class HouseAPIController : ControllerBase
    {
        private readonly IHouseRepository _dbHouse;
        private readonly IMapper _mapper;

        public HouseAPIController(IHouseRepository dbHouse, IMapper mapper)
        {
            
            _dbHouse = dbHouse;
            _mapper = mapper;
        } 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HouseDTO>>> GetHouses()
        {

            IEnumerable<House>houseList = await _dbHouse.GetAllAsync();
            return Ok(_mapper.Map<List<HouseDTO>>(houseList));


        }


        [HttpGet("{id:int}", Name = "GetHouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HouseDTO>> GetHouse(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var house = await _dbHouse.GetAsync(u => u.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HouseDTO>(house));


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HouseDTO>> CreateHouse([FromBody] HouseCreateDTO createDTO)
        {
            //if(!ModelState.IsValid) 
            //{
            //    return BadRequest(ModelState);
            //}

            if (await _dbHouse.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "House already exists!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            //if (houseDTO.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            House model = _mapper.Map<House>(createDTO);

            //House model = new House()
            //{
            //    Amenity = createDTO.Amenity,
            //    Details = createDTO.Details,
            
            //    ImageUrl = createDTO.ImageUrl,
            //    Name = createDTO.Name,
            //    Occupancy = createDTO.Occupancy,
            //    Rate = createDTO.Rate,
            //    Sqft = createDTO.Sqft,
            //};

            //await _db.Houses.AddAsync(model);
            //await _db.SaveChangesAsync();
            await _dbHouse.CreateAsync(model);
            return CreatedAtRoute("GetHouse", new { id = model.Id }, model);
        }


        [HttpDelete("{id:int}", Name = "DeleteHouse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteHouse(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var house = await _dbHouse.GetAsync(u => u.Id == id);
            if (house == null)
            {
                return NotFound();
            }
            //_db.Houses.Remove(house);
            //await _db.SaveChangesAsync();
            await _dbHouse.RemoveAsync(house);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateHouse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        

        public async Task<IActionResult> UpdateHouse(int id, [FromBody] HouseUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }

            House model = _mapper.Map<House>(updateDTO);

            // _db.Houses.Update(model);
            //await _db.SaveChangesAsync();

            await _dbHouse.UpdateAsync(model);

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialHouse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialHouse(int id,JsonPatchDocument<HouseUpdateDTO>patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var house = await _dbHouse.GetAsync(u => u.Id == id,tracked:false);

            HouseUpdateDTO houseDTO = _mapper.Map<HouseUpdateDTO>(house);

            //HouseUpdateDTO houseDTO = new ()
            //{
            //    Amenity = house.Amenity,
            //    Details = house.Details,
            //    Id = house.Id,
            //    ImageUrl = house.ImageUrl,
            //    Name = house.Name,
            //    Occupancy = house.Occupancy,
            //    Rate = house.Rate,
            //    Sqft = house.Sqft,
            //};
            if (house == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(houseDTO, ModelState);
            House model = _mapper.Map<House>(houseDTO);

            //_db.Houses.Update(model);
            //await _db.SaveChangesAsync();

            await _dbHouse.UpdateAsync(model);

            if (!ModelState.IsValid) 
            {
                return BadRequest();

            }
            return NoContent();
        }

    }



}
