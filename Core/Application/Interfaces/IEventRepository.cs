namespace Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEventRepository : IBaseRepository<Event>
{
    Task<List<Event>> GetEventsSortedByDateAsync(bool ascending = true);
    Task<List<Event>> GetEventsByDateAsync(DateTime date);
    Task<List<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<Event>> GetEventsByPriceRangeAsync(int minPrice, int maxPrice);
    Task<List<Event>> GetEventsByPriceRangeAndDateRangeAsync(int minPrice, int maxPrice, DateTime startDate, DateTime endDate);
    Task<List<Event>> GetEventsByGameAsync(string game);
}