using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    using Domain.Entities;

    public interface IUserService
    {
        Task<User> GetUserByLoginAsync(string login);
    }
}
