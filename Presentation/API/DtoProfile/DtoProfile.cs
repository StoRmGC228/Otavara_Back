namespace API.DtoProfile;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;

public class DtoProfile : Profile
{
    public DtoProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
    }
}