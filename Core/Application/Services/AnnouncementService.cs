namespace Application.Services;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class AnnouncementService : BaseService<Announcement>, IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IMapper _mapper;

    public AnnouncementService(IAnnouncementRepository announcementRepository, IMapper mapper) : base(
        announcementRepository, mapper)
    {
        _announcementRepository = announcementRepository;
        _mapper = mapper;
    }

    public async Task<Announcement> AddAsync(Announcement entity)
    {
        return await _announcementRepository.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _announcementRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Announcement>> GetAllAsync()
    {
        return await _announcementRepository.GetAllAsync();
    }

    public async Task<Announcement?> GetByIdAsync(Guid id)
    {
        return await _announcementRepository.GetByIdAsync(id);
    }

    public async Task<PaginatedDto<Announcement>> GetPaginateAsync(int pageSize, int pageNumber)
    {
        return await _announcementRepository.GetPaginatedAsync(pageSize, pageNumber);
    }

    public async Task<IEnumerable<Announcement>> GetUserAnnouncementsAsync(Guid userId)
    {
        return await _announcementRepository.GetByRequesterIdAsync(userId);
    }

    public async Task DeleteOverdueAnnouncements()
    {
        await _announcementRepository.DeleteOverdueAnnouncements();
    }

    public async Task<Announcement> UpdateAsync(Guid id, Announcement entity)
    {
        return await _announcementRepository.UpdateAsync(id, entity);
    }
}