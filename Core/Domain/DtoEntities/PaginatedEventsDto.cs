namespace Domain.DtoEntities;

public class PaginatedEventsDto
{
    public int TotalPages { get; set; }
    public List<EventCreationDto> PaginatedEntities { get; set; }
}