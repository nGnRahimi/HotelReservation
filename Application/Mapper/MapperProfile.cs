
using Application.Features.Search.Dto;
using AutoMapper;
using Domain.Models.Hotels;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//این کلاس عملیات مپ کردنو کنترل میکنه

namespace Application.Mapper
{
    
    public class MapperProfile : Profile
    { 
        public MapperProfile() 
        {
            CreateMap<Hotel , HotelInfoDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.HotelGalleries.Select(g => g.Path).ToList()));
        }
    }
}
