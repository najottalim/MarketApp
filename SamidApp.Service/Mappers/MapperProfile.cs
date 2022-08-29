using AutoMapper;
using SamidApp.Domain.Entities.Products;
using SamidApp.Service.DTOs;

namespace SamidApp.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Product, ProductForCreationDto>().ReverseMap();
    }
}