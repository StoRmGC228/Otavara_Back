namespace Domain.DtoEntities;

using System.Text.Json.Serialization;

public class TelegramUserDto
{
    [JsonPropertyName("id")] public int TelegramId { get; set; }

    [JsonPropertyName("first_name")] public string? FirstName { get; set; }

    [JsonPropertyName("last_name")] public string? LastName { get; set; }

    public string? Username { get; set; }

    [JsonPropertyName("photo_url")] public string PhotoUrl { get; set; }

    public string Hash { get; set; }

    [JsonPropertyName("auth_date")] public int AuthDate { get; set; }
}