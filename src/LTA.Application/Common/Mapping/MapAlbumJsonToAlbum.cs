using AutoMapper;
using LTA.Domain.Entities;
using LTA.Domain.Entities.Json;

namespace LTA.Application.Common.Mapping;

public class MapAlbumJsonToAlbum : Profile
{
    public MapAlbumJsonToAlbum()
    {
        CreateMap<AlbumJson, Album>();
    }
}