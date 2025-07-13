using System.Text.Json.Serialization;

namespace Domain.DtoEntities;

public class AnnouncementDto
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public DateOnly RequestedDate { get; set; }
    public CardDto Card { get; set; }
    public string RequesterTag { get; set; }
}