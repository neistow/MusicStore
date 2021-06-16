using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Entities;

namespace Catalog.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Album, AlbumDto>();
            CreateMap<Genre, GenreDto>();
        }
    }
}