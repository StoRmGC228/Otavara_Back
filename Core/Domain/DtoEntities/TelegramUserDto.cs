namespace Domain.DtoEntities;

public class TelegramUserDto
{
    public int Id { get; set; }
    public string? First_name { get; set; }
    public string? Last_name { get; set; }
    public string? Username { get; set; }
    public string Photo_url { get; set; }
    public string Hash { get; set; }
    public int Auth_date { get; set; }
}