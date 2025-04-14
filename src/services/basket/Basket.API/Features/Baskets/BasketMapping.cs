using AutoMapper;
using Basket.API.Data;
using Basket.API.DTOs;

namespace Basket.API.Features.Baskets;

public class BasketMapping : Profile
{
    public BasketMapping() {
        CreateMap<BasketDto, Data.Basket>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>().ReverseMap();
    }
}