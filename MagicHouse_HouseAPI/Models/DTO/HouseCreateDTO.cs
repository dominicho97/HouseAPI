﻿using System.ComponentModel.DataAnnotations;

namespace MagicHouse_HouseAPI.Models.DTO
{
    public class HouseCreateDTO
    {


        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Details { get; set; }

        public double Rate { get; set; }

        public int Occupancy { get; set; }

        public int Sqft { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
