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
            .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramId));
        CreateMap<EventWithIdDto, Event>()
            .ForMember(dest => dest.EventStartTime,
                opt => opt.MapFrom(src => new DateTime(src.EventDate.Year, src.EventDate.Month, src.EventDate.Day,
                    src.EventTime.Hour, src.EventTime.Minute, src.EventTime.Second)));
        CreateMap<Event, EventWithIdDto>()
            .ForMember(dest => dest.EventDate,
                opt => opt.MapFrom(src => DateOnly.FromDateTime(src.EventStartTime)))
            .ForMember(dest => dest.EventTime,
                opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.EventStartTime)));
        CreateMap<EventWithoutIdDto, Event>()
            .ForMember(dest => dest.EventStartTime,
                opt => opt.MapFrom(src => new DateTime(src.EventDate.Year, src.EventDate.Month, src.EventDate.Day,
                    src.EventTime.Hour, src.EventTime.Minute, src.EventTime.Second)));
        CreateMap<Event, EventWithoutIdDto>()
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

        CreateMap<Good, Good>().ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Event, Event>().ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Announcement, Announcement>().ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<BookedGood, BookedGoodDto>();
        CreateMap<BookedGood, BookerDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.User.PhotoUrl))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));
        CreateMap<Good, GoodAdminDto>().ReverseMap()
            .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings));
        CreateMap<Good, GoodDto>();
        CreateMap<GoodCreationDto, Good>();
        CreateMap<User, UserGetDto>();
        CreateMap<UserGetDto, User>();
        CreateMap<EventForCreationAndUpdateDto, Event>()
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.EventStartTime,
                opt => opt.MapFrom(src =>
                    DateTime.Parse(src.Date.ToString("yyyy-MM-dd") + "T" + src.Time.ToString("HH:mm"))));
    }
}