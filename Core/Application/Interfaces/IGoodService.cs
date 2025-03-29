using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGoodService
    {
        Task<Good> CreateGood(Good good);
        Task<Good> UpdateGood(Guid id,Good good);
        Task<Good> GetGoodById(Guid goodId);
        Task DeleteGood(Guid Id);
        //Task<Good> GetGoodByName(string goodName);
        Task<IEnumerable<Good>> GetAllGoods();
        Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending);
        Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending);
        Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending);

    }
}
