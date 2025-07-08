namespace Application.Interfaces;

using Domain.Entities;

public interface IParticipantsRepository
{
    Task<List<Participant>> GetEventParticipantsAsync(Guid eventId);
    Task<List<Event>> GetUserEventsAsync(Guid userId);
    Task AddParticipantAsync(Guid eventId, Guid userId);
    Task RemoveParticipantAsync(Guid eventId, Guid userId);
    Task<bool> IsUserParticipantAsync(Guid eventId, Guid userId);
    Task<int> GetEventParticipantsCountAsync(Guid eventId);
}