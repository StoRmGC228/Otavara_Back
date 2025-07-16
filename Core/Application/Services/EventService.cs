namespace Application.Services;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class EventService : BaseService<Event>, IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IImageUploader _imageUploader;
    private readonly IMapper _mapper;
    private readonly IParticipantsRepository _participantsRepository;

    public EventService(IEventRepository eventRepository, IParticipantsRepository participantsRepository,
        IMapper mapper, IImageUploader imageUploader) : base(
        eventRepository, mapper)
    {
        _eventRepository = eventRepository;
        _participantsRepository = participantsRepository;
        _mapper = mapper;
        _imageUploader = imageUploader;
    }

    public async Task<Event> AddAsync(EventCreationDto entity)
    {
        var mappedEvent = _mapper.Map<Event>(entity);
        mappedEvent.Id = Guid.NewGuid();
        return await _eventRepository.AddAsync(mappedEvent);
    }

    public async Task<Event> UpdateAsync(EventCreationDto newEvent, Guid id)
    {
        var existing = await _eventRepository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException($"Event {id} not found.");

        _mapper.Map(newEvent, existing);

        //if (newEvent.Image != null)
        //{
        //    existing.Image = await _imageUploader.UploadImageAsync(newEvent.Image);
        //}
        //else if (!string.IsNullOrEmpty(newEvent.ImageUrl))
        //{
        //    existing.Image = newEvent.ImageUrl;
        //}

        return await _eventRepository.UpdateAsync(id, existing);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _eventRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    // Event-specific methods

    public async Task<List<string>> GetEventImagesFromCloudAsync()
    {
        return await _imageUploader.GetPublicIdOfAllImagesAsync();
    }


    public async Task<List<Event>> GetEventsByDateRangeAsync(DateTime? startDate, DateTime? endDate)
    {
        return await _eventRepository.GetEventsByDateRangeAsync(startDate, endDate);
    }

    public async Task<List<Event>> GetEventsByPriceRangeAsync(int minPrice, int maxPrice)
    {
        return await _eventRepository.GetEventsByPriceRangeAsync(minPrice, maxPrice);
    }

    public async Task<List<Event>> GetEventsByPriceRangeAndDateRangeAsync(int minPrice, int maxPrice,
        DateTime startDate, DateTime endDate)
    {
        return await _eventRepository.GetEventsByPriceRangeAndDateRangeAsync(minPrice, maxPrice, startDate, endDate);
    }

    public async Task<List<Participant>> GetEventParticipantsAsync(Guid eventId)
    {
        return await _participantsRepository.GetEventParticipantsAsync(eventId);
    }

    public async Task AddParticipantAsync(Guid eventId, Guid userId)
    {
        await _participantsRepository.AddParticipantAsync(eventId, userId);
    }

    public async Task RemoveParticipantAsync(Guid eventId, Guid userId)
    {
        await _participantsRepository.RemoveParticipantAsync(eventId, userId);
    }

    public async Task<int> GetEventParticipantsCountAsync(Guid eventId)
    {
        return await _participantsRepository.GetEventParticipantsCountAsync(eventId);
    }

    public async Task<bool> IsUserParticipantAsync(Guid eventId, Guid userId)
    {
        return await _participantsRepository.IsUserParticipantAsync(eventId, userId);
    }

    public async Task<List<Event>> GetUserEventsAsync(Guid userId)
    {
        return await _participantsRepository.GetUserEventsAsync(userId);
    }

    public async Task<List<Event>>? GetEventsByNameAndDateRangeAsync(string name, DateTime? startDate,
        DateTime? endDate)
    {
        return await _eventRepository.GetEventsByNameAndDateRangeAsync(name, startDate, endDate);
    }

    public async Task<Event> UpdateAsync(Guid id, Event entity)
    {
        return await _eventRepository.UpdateAsync(id, entity);
    }
}