namespace Application.Services;

using Domain.Entities;
using Interfaces;

public class EventService : BaseService<Event>, IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IParticipantsRepository _participantsRepository;

    public EventService(IEventRepository eventRepository, IParticipantsRepository participantsRepository) : base(
        eventRepository)
    {
        _eventRepository = eventRepository;
        _participantsRepository = participantsRepository;
    }

    public async Task<Event> AddAsync(Event entity)
    {
        return await _eventRepository.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _eventRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    public async Task<Event> UpdateAsync(Event entity)
    {
        return await _eventRepository.UpdateAsync(entity);
    }

    // Event-specific methods
    public async Task<List<Event>> GetEventsByDateAsync(DateTime date)
    {
        return await _eventRepository.GetEventsByDateAsync(date);
    }

    public async Task<List<Event>> GetEventsByGameAsync(string game)
    {
        return await _eventRepository.GetEventsByGameAsync(game);
    }

    public async Task<List<Event>> GetEventsSortedByDateAsync(bool ascending = true)
    {
        return await _eventRepository.GetEventsSortedByDateAsync(ascending);
    }

    public async Task<List<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _eventRepository.GetEventsByDateRangeAsync(startDate, endDate);
    }

    public async Task<List<Event>> GetEventsByPriceRangeAsync(int minPrice, int maxPrice)
    {
        return await _eventRepository.GetEventsByPriceRangeAsync(minPrice, maxPrice);
    }

    public async Task<List<Event>> GetEventsByPriceRangeAndDateRangeAsync(int minPrice, int maxPrice,
        DateTime startDate, DateTime endDate)
    {
        return await _eventRepository.GetEventsByPriceRangeAndDateRangeAsync(minPrice, maxPrice, startDate, endDate);
    }

    public async Task<List<User>> GetEventParticipantsAsync(Guid eventId)
    {
        return await _participantsRepository.GetEventParticipantsAsync(eventId);
    }

    public async Task AddParticipantAsync(Guid eventId, Guid userId)
    {
        await _participantsRepository.AddParticipantAsync(eventId, userId);
    }

    public async Task RemoveParticipantAsync(Guid eventId, Guid userId)
    {
        await _participantsRepository.RemoveParticipantAsync(eventId, userId);
    }

    public async Task<int> GetEventParticipantsCountAsync(Guid eventId)
    {
        return await _participantsRepository.GetEventParticipantsCountAsync(eventId);
    }

    public async Task<bool> IsUserParticipantAsync(Guid eventId, Guid userId)
    {
        return await _participantsRepository.IsUserParticipantAsync(eventId, userId);
    }

    public async Task<List<Event>> GetUserEventsAsync(Guid userId)
    {
        return await _participantsRepository.GetUserEventsAsync(userId);
    }
}