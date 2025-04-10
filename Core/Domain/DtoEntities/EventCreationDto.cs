namespace Domain.DtoEntities;

using System.Text.Json.Serialization;
using Entities;

public class EventCreationDto
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public string? Format { get; set; }
    public string Game { get; set; }
    [JsonPropertyName("participants")]public List<ParticipantForEventDto> Participants { get; set; }

    [JsonPropertyName("date")] public DateOnly EventDate { get; set; }

    [JsonPropertyName("time")] public TimeOnly EventTime { get; set; }
}