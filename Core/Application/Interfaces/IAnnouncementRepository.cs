namespace Application.Interfaces;

using Domain.Entities;

public interface IAnnouncementRepository : IBaseRepository<Announcement>
{
    Task<IEnumerable<Announcement>> GetByRequesterIdAsync(Guid requesterId);
    Task DeleteOverdueAnnouncements();
}