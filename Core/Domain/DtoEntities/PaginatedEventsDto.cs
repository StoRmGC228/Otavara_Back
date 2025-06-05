namespace Domain.DtoEntities;

public class PaginatedEventsDto
{
    public int TotalPages { get; set; }
    public List<EventDto> PaginatedEntities { get; set; }
}