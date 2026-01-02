namespace Domain.DtoEntities;
//test-test
public class AnnouncementCreationDto
{
    public int Count { get; set; }
    public DateOnly RequestedDate { get; set; }
    public CardDto Card { get; set; }
    public string RequesterTag { get; set; }
}