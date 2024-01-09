﻿using MagicHouse_HouseAPI.Data;

using MagicHouse_HouseAPI.Models;
using MagicHouse_HouseAPI.Models.DTO;
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
        private readonly ApplicationDbContext _db;

        public HouseAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HouseDTO>> GetHouses()
        {
            
            return Ok(_db.Houses);


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
            var house = _db.Houses.FirstOrDefault(u => u.Id == id);
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

            if (_db.Houses.FirstOrDefault(u => u.Name.ToLower() == houseDTO.Name.ToLower()) != null)
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
            House model = new House()
            {
                Amenity = houseDTO.Amenity,
                Details = houseDTO.Details,
                Id = houseDTO.Id,
                ImageUrl = houseDTO.ImageUrl,
                Name = houseDTO.Name,
                Occupancy = houseDTO.Occupancy,
                Rate = houseDTO.Rate,
                Sqft = houseDTO.Sqft,
            };
            _db.Houses.Add(model);
            _db.SaveChanges();

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
            var house = _db.Houses.FirstOrDefault(u => u.Id == id);
            if (house == null)
            {
                return NotFound();
            }
            _db.Houses.Remove(house);
            _db.SaveChanges();
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
            /*     var house = _db.Houses.FirstOrDefault(u => u.Id == id);
                 house.Name = houseDTO.Name;
                 house.Sqft = houseDTO.Sqft;
                 house.Occupancy = houseDTO.Occupancy;*/

            House model = new House()
            {
                Amenity = houseDTO.Amenity,
                Details = houseDTO.Details,
                Id = houseDTO.Id,
                ImageUrl = houseDTO.ImageUrl,
                Name = houseDTO.Name,
                Occupancy = houseDTO.Occupancy,
                Rate = houseDTO.Rate,
                Sqft = houseDTO.Sqft,
            };
            _db.Houses.Update(model);
            _db.SaveChanges();

            return NoContent();
        }



        [HttpPatch("{id:int}", Name = "UpdatePartialHouse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialHouse(int id,JsonPatchDocument<HouseDTO>patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var house = _db.Houses.AsNoTracking().FirstOrDefault(u => u.Id == id);


            HouseDTO houseDTO = new HouseDTO()
            {
                Amenity = house.Amenity,
                Details = house.Details,
                Id = house.Id,
                ImageUrl = house.ImageUrl,
                Name = house.Name,
                Occupancy = house.Occupancy,
                Rate = house.Rate,
                Sqft = house.Sqft,
            };
            if (house == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(houseDTO, ModelState);
            House model = new House()
            {
                Amenity = houseDTO.Amenity,
                Details = houseDTO.Details,
                Id = houseDTO.Id,
                ImageUrl = houseDTO.ImageUrl,
                Name = houseDTO.Name,
                Occupancy = houseDTO.Occupancy,
                Rate = houseDTO.Rate,
                Sqft = houseDTO.Sqft,
            };
            _db.Houses.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid) 
            {
                return BadRequest();

            }
            return NoContent();
        }

    }



}
