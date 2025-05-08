namespace Application.Interfaces;

using Domain.Entities;

public interface IAnnouncementService : IBaseService<Announcement>
{
    Task<IEnumerable<Announcement>> GetUserAnnouncementsAsync(Guid userId);
}