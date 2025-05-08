namespace Domain.DtoEntities;

using System.Text.Json.Serialization;

public class EventWithIdDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public string? Format { get; set; }
    [JsonPropertyName("participants")] public List<ParticipantForEventDto>? Participants { get; set; }
    [JsonPropertyName("date")] public DateOnly EventDate { get; set; }
    [JsonPropertyName("time")] public TimeOnly EventTime { get; set; }
}