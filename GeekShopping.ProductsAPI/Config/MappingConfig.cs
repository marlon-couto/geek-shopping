using AutoMapper;
using GeekShopping.ProductsAPI.Data.ValueObjects;
using GeekShopping.ProductsAPI.Models;

namespace GeekShopping.ProductsAPI.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(mc => { mc.CreateMap<ProductVO, Product>().ReverseMap(); });
    }
}