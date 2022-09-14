using AutoMapper;
using SamidApp.Domain.Entities.Products;
using SamidApp.Domain.Entities.Users;
using SamidApp.Service.DTOs;
using SamidApp.Service.DTOs.Users;

namespace SamidApp.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ProductForCreationDto, Product>()
            .ForMember(p => p.File, config => config.Ignore())
            .ReverseMap();
        CreateMap<UserForCreationDto, User>().ReverseMap();
    }
}