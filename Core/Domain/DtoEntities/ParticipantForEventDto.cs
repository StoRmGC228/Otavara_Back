namespace Domain.DtoEntities
{
    using System.Text.Json.Serialization;

    public class ParticipantForEventDto
    {
        public Guid Id { get; set; }
        [JsonPropertyName("photo_url")] public string PhotoUrl { get; set; }
        [JsonPropertyName("name")] public string Username { get; set; }
    }
}
