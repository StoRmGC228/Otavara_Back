using System.Text.Json.Serialization;

namespace Domain.DtoEntities;

public class AnnouncementDto
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public DateOnly RequestedDate { get; set; }
    [JsonIgnore] public Guid RequesterId { get; set; }
    public CardDto Card { get; set; }
}