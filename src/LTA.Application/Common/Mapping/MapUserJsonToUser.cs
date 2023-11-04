using AutoMapper;
using LTA.Domain.Entities;
using LTA.Domain.Entities.Json;

namespace LTA.Application.Common.Mapping;

public class MapUserJsonToUser : Profile
{
    public MapUserJsonToUser()
    {
        CreateMap<UserJson, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.AddressStreet, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.AddressSuite, opt => opt.MapFrom(src => src.Address.Suite))
            .ForMember(dest => dest.AddressCity, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.AddressZipcode, opt => opt.MapFrom(src => src.Address.Zipcode))
            .ForMember(dest => dest.AddressLat, opt => opt.MapFrom(src => src.Address.Geo.Lat))
            .ForMember(dest => dest.AddressLng, opt => opt.MapFrom(src => src.Address.Geo.Lng))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
            .ForMember(dest => dest.CompanyCatchPhrase, opt => opt.MapFrom(src => src.Company.CatchPhrase))
            .ForMember(dest => dest.CompanyBs, opt => opt.MapFrom(src => src.Company.Bs));
    }
}