namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    private readonly OtavaraDbContext _context;
    private readonly DbSet<Event> _eventDb;
    private readonly DbSet<Participant> _participantDb;

    public EventRepository(OtavaraDbContext context) : base(context)
    {
        _context = context;
        _eventDb = context.Set<Event>();
        _participantDb = context.Set<Participant>();
    }

    public async Task<List<User>> GetEventParticipantsAsync(Guid eventId)
    {
        return await _participantDb
            .Where(p => p.EventId == eventId)
            .Select(p => p.User)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsSortedByDateAsync(bool ascending = true)
    {
        if (ascending)
        {
            return await _eventDb.OrderBy(e => e.EventStartTime).ToListAsync();
        }
        else
        {
            return await _eventDb.OrderByDescending(e => e.EventStartTime).ToListAsync();
        }
    }

    public async Task<List<Event>> GetEventsByDateAsync(DateTime date)
    {
        return await _eventDb
            .Where(e => e.EventStartTime.Date == date.Date)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _eventDb
            .Where(e => e.EventStartTime.Date >= startDate.Date && e.EventStartTime.Date <= endDate.Date)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByPriceRangeAsync(int minPrice, int maxPrice)
    {
        return await _eventDb
            .Where(e => e.Price >= minPrice && e.Price <= maxPrice)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByPriceRangeAndDateRangeAsync(
        int minPrice, int maxPrice, DateTime startDate, DateTime endDate)
    {
        return await _eventDb
            .Where(e => e.Price >= minPrice && e.Price <= maxPrice && e.EventStartTime.Date >= startDate.Date && e.EventStartTime.Date <= endDate.Date)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByGameAsync(string game)
    {
        return await _eventDb
            .Where(e => e.Game.ToLower().Contains(game.ToLower()))
            .ToListAsync();
    }

    public async Task AddParticipantAsync(Guid eventId, Guid userId)
    {
        var participant = new Participant { EventId = eventId, UserId = userId };
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
}
