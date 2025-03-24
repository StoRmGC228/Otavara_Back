namespace Domain.DtoEntities;

public class UserDto
{
    public int TelegramId { get; set; }
    public string TelegramFirstName { get; set; }
    public string TelegramUserName { get; set; }
    public string HashPassword { get; set; }
    public string PhotoURL { get; set; }
}