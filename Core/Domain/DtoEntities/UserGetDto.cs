namespace Domain.DtoEntities;

using System.Text.Json.Serialization;

public class UserGetDto
{
    public Guid Id { get; set; }
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("photo_url")] public string PhotoUrl { get; set; }
    public string Role { get; set; }
    public string Username { get; set; }
}