using AutoMapper;
using MagicHouse_HouseAPI.Models;
using MagicHouse_HouseAPI.Models.DTO;

namespace MagicHouse_HouseAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig() 
        {
            CreateMap<House, HouseDTO>();
            CreateMap<HouseDTO, House >();

            CreateMap<House, HouseCreateDTO>().ReverseMap();
            CreateMap<House, HouseUpdateDTO>().ReverseMap();
        }
    }
}
