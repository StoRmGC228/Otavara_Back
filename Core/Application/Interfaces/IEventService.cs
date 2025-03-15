namespace Application.Interfaces;

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEventService : IBaseService<Event>
{
    Task<List<Event>> GetEventsByDateAsync(DateTime date);
    Task<List<Event>> GetEventsByGameAsync(string game);
    Task<List<Event>> GetEventsSortedByDateAsync(bool ascending = true);
    Task<List<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<Event>> GetEventsByPriceRangeAsync(int minPrice, int maxPrice);
    Task<List<Event>> GetEventsByPriceRangeAndDateRangeAsync(int minPrice, int maxPrice, DateTime startDate, DateTime endDate);

    Task<List<User>> GetEventParticipantsAsync(Guid eventId);
    Task AddParticipantAsync(Guid eventId, Guid userId);
    Task RemoveParticipantAsync(Guid eventId, Guid userId);
    Task<int> GetEventParticipantsCountAsync(Guid eventId);
    Task<bool> IsUserParticipantAsync(Guid eventId, Guid userId);
    Task<List<Event>> GetUserEventsAsync(Guid userId);
}