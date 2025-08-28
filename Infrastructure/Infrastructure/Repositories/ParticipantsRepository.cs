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

    public async Task<List<Participant>> GetEventParticipantsAsync(Guid eventId)
    {
        return await _participantDb
            .Where(p => p.EventId == eventId)
            .ToListAsync();
    }

    public async Task<List<Event>> GetUserEventsAsync(Guid userId)
    {
        return await _participantDb
            .Where(p => p.UserId == userId)
            .Select(p => p.Event)
            .ToListAsync();
    }

    public async Task<Event> AddParticipantAsync(Guid eventId, Guid userId)
    {
        var user = _userDb.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            throw new Exception("Користувач не знайдений");

        var tournament = await _context.Events
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        if (tournament == null)
            throw new Exception("Турнір не знайдений");

        if (tournament.Participants != null && tournament.Participants.Any(p => p.UserId == userId))
            throw new Exception("Користувач вже записаний на цей турнір");

        // перевірка на максимальну кількість
        if (tournament.MaxParticipants > 0 &&
            tournament.Participants!.Count >= tournament.MaxParticipants)
        {
            throw new Exception("Досягнуто максимальну кількість учасників");
        }

        var participant = new Participant
        {
            EventId = eventId,
            UserId = userId,
            PhotoUrl = user.PhotoUrl,
            Username = user.Username
        };

        await _participantDb.AddAsync(participant);
        await _context.SaveChangesAsync();

        return tournament;
    }


    public async Task<Event> RemoveParticipantAsync(Guid eventId, Guid userId)
    {
        var participantToRemove = await _participantDb.FirstOrDefaultAsync(
            p => p.EventId == eventId && p.UserId == userId);
        if (participantToRemove != null)
        {
            _participantDb.Remove(participantToRemove);
            await _context.SaveChangesAsync();
        }

        var tournament = await _context.Events
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        return tournament ?? new Event();
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