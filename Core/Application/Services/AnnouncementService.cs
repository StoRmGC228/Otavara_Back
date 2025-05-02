using Application.Interfaces;
using Domain.DtoEntities;
using Domain.Entities;

namespace Application.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _requestedCardRepository;

        public AnnouncementService(IAnnouncementRepository requestedCardRepository)
        {
            _requestedCardRepository = requestedCardRepository;
        }

        public async Task<Announcement> AddAsync(Announcement entity)
        {
            return await _requestedCardRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _requestedCardRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _requestedCardRepository.GetAllAsync();
        }

        public async Task<Announcement?> GetByIdAsync(Guid id)
        {
            return await _requestedCardRepository.GetByIdAsync(id);
        }

        public async Task<PaginatedDto<Announcement>> GetPaginateAsync(int pageSize, int pageNumber)
        {
            return await _requestedCardRepository.GetPaginatedAsync(pageSize, pageNumber);
        }

        public async Task<IEnumerable<Announcement>> GetUserAnnouncementsAsync(Guid userId)
        {
            return await _requestedCardRepository.GetByRequesterIdAsync(userId);
        }

        public async Task<Announcement> UpdateAsync(Guid id, Announcement entity)
        {
            return await _requestedCardRepository.UpdateAsync(id,entity);
        }
    }
}
