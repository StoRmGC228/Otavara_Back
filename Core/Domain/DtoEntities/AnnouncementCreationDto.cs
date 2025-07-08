namespace Domain.DtoEntities;

public class AnnouncementCreationDto
{
    public int Count { get; set; }
    public DateOnly RequestedDate { get; set; }
    public CardDto Card { get; set; }
}