using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAnnouncementService : IBaseService<Announcement>
    {
        Task<IEnumerable<Announcement>> GetUserAnnouncementsAsync(Guid userId);
    }
}
