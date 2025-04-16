namespace API.DtoProfile;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;

public class DtoProfile : Profile
{
    public DtoProfile()
    {
        CreateMap<TelegramUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<User, TelegramUserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TelegramId));
        CreateMap<EventCreationDto, Event>()
            .ForMember(dest => dest.EventStartTime,
                opt => opt.MapFrom(src => new DateTime(src.EventDate.Year, src.EventDate.Month, src.EventDate.Day,
                    src.EventTime.Hour, src.EventTime.Minute, src.EventTime.Second).ToUniversalTime()));
        CreateMap<Event, EventCreationDto>()
            .ForMember(dest => dest.EventDate,
                opt => opt.MapFrom(src => DateOnly.FromDateTime(src.EventStartTime)))
            .ForMember(dest => dest.EventTime,
                opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.EventStartTime)));
        CreateMap<ParticipantForEventDto, Participant>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        CreateMap<Participant, ParticipantForEventDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));

        CreateMap<Card, CardDto>().ReverseMap();

        CreateMap<AnnouncementDto, Announcement>()
            .ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card))
            .ForMember(dest => dest.CardId, opt => new Guid());

        CreateMap<Announcement, AnnouncementDto>()
            .ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card));
    }
}