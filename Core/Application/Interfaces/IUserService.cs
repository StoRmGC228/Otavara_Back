namespace Application.Interfaces;

using Domain.Entities;

public interface IUserService : IBaseService<User>
{
    Task<User> GetUserByLoginAsync(string login);
}