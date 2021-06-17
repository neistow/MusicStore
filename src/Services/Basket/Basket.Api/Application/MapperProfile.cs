using AutoMapper;
using Basket.Api.Domain;
using Basket.Api.Responses;

namespace Basket.Api.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Basket, BasketResponse>();
            CreateMap<BasketItem, BasketItemResponse>();
        }
    }
}