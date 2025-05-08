namespace Domain.DtoEntities;

public class PaginatedEventsDto
{
    public int TotalPages { get; set; }
    public List<EventWithIdDto> PaginatedEntities { get; set; }
}