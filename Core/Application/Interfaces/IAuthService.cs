namespace Application.Interfaces;

using Domain.DtoEntities;

public interface IAuthService
{
    Task<string> LoginUserAsync(TelegramUserDto loginUser);
    Task<bool> VerifyDataCheckStringAsync(string telegramHash, string botTokenHash);
}