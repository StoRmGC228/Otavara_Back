namespace Application.Interfaces;

using Domain.Entities;

public interface IParticipantsRepository
{
    Task<List<Participant>> GetEventParticipantsAsync(Guid eventId);
    Task<List<Event>> GetUserEventsAsync(Guid userId);
    Task<Event> AddParticipantAsync(Guid eventId, Guid userId);
    Task<Event> RemoveParticipantAsync(Guid eventId, Guid userId);
    Task<bool> IsUserParticipantAsync(Guid eventId, Guid userId);
    Task<int> GetEventParticipantsCountAsync(Guid eventId);
}