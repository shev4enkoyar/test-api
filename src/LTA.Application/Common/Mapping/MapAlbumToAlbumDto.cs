using AutoMapper;
using LTA.Application.Albums.Queries;
using LTA.Domain.Entities;

namespace LTA.Application.Common.Mapping;

public class MapAlbumToAlbumDto : Profile
{
    public MapAlbumToAlbumDto()
    {
        CreateMap<Album, AlbumDto>();
    }
}