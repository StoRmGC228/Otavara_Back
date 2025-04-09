namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ParticipantsRepository : IParticipantsRepository
{
    private readonly OtavaraDbContext _context;
    private readonly DbSet<Participant> _participantDb;
    private readonly DbSet<User> _userDb;

    public ParticipantsRepository(OtavaraDbContext context)
    {
        _context = context;
        _participantDb = context.Set<Participant>();
        _userDb = context.Set<User>();
    }

    public async Task<List<User>> GetEventParticipantsAsync(Guid eventId)
    {
        return await _participantDb
            .Where(p => p.EventId == eventId)
            .Select(p => p.User)
            .ToListAsync();
    }

    public async Task<List<Event>> GetUserEventsAsync(Guid userId)
    {
        return await _participantDb
            .Where(p => p.UserId == userId)
            .Select(p => p.Event)
            .ToListAsync();
    }

    public async Task AddParticipantAsync(Guid eventId, Guid userId)
    {
        var user =  _userDb.FirstOrDefault(u => u.Id == userId);
        var participant = new Participant { EventId = eventId, UserId = userId, PhotoUrl = user.PhotoUrl,Username =user.Username};
        await _participantDb.AddAsync(participant);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveParticipantAsync(Guid eventId, Guid userId)
    {
        var participantToRemove = await _participantDb.FirstOrDefaultAsync(
            p => p.EventId == eventId && p.UserId == userId);
        if (participantToRemove != null)
        {
            _participantDb.Remove(participantToRemove);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsUserParticipantAsync(Guid eventId, Guid userId)
    {
        return await _participantDb.AnyAsync(p => p.EventId == eventId && p.UserId == userId);
    }

    public async Task<int> GetEventParticipantsCountAsync(Guid eventId)
    {
        return await _participantDb.CountAsync(p => p.EventId == eventId);
    }
}