namespace Application.Interfaces;

using Domain.DtoEntities;
using Domain.Entities;

public interface IEventService : IBaseService<Event>
{
    Task<Event> AddAsync(EventCreationDto newEvent);
    Task<Event> UpdateAsync(EventCreationDto newEvent, Guid id);
    Task<List<string>> GetEventImagesFromCloudAsync();
    Task<List<Event>> GetEventsByDateRangeAsync(DateTime? startDate, DateTime? endDate);
    Task<List<Event>> GetEventsByPriceRangeAsync(int minPrice, int maxPrice);

    Task<List<Event>> GetEventsByPriceRangeAndDateRangeAsync(int minPrice, int maxPrice, DateTime startDate,
        DateTime endDate);

    Task<List<Participant>> GetEventParticipantsAsync(Guid eventId);
    Task<Event> AddParticipantAsync(Guid eventId, Guid userId);
    Task<Event> RemoveParticipantAsync(Guid eventId, Guid userId);
    Task<int> GetEventParticipantsCountAsync(Guid eventId);
    Task<bool> IsUserParticipantAsync(Guid eventId, Guid userId);
    Task<List<Event>> GetUserEventsAsync(Guid userId);

    Task<List<Event>>? GetEventsByNameAndDateRangeAsync(string name, DateTime? startDate, DateTime? endDate);
}