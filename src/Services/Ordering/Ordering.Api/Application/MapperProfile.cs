using AutoMapper;
using Ordering.Api.Domain;
using Ordering.Api.Responses;

namespace Ordering.Api.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Checkout, CheckoutResponse>();
            CreateMap<CheckoutItem, CheckoutItemResponse>();
        }
    }
}