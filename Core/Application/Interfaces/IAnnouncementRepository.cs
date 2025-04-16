using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAnnouncementRepository : IBaseRepository<Announcement>
    {
        Task<IEnumerable<Announcement>> GetByRequesterIdAsync(Guid requesterId);
    }
}
