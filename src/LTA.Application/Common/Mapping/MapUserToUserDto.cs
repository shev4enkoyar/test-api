using AutoMapper;
using LTA.Application.Users.Queries;
using LTA.Domain.Entities;

namespace LTA.Application.Common.Mapping;

public class MapUserToUserDto : Profile
{
    public MapUserToUserDto()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src => new AddressDto
                {
                    Street = src.AddressStreet,
                    Suite = src.AddressSuite,
                    City = src.AddressCity,
                    Zipcode = src.AddressZipcode,
                    Geo = new GeoDto
                    {
                        Lat = src.AddressLat,
                        Lng = src.AddressLng
                    }
                }))
            .ForMember(dest => dest.Company,
                opt => opt.MapFrom(src => new CompanyDto
                {
                    Name = src.CompanyName,
                    CatchPhrase = src.CompanyCatchPhrase,
                    Bs = src.CompanyBs
                }));
    }
}