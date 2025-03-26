using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGoodRepository : IBaseRepository<Good>
    {   
        Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending); // true - orderby, false - orderby descending
        Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending);// true - from largest to smallest quantity
        Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending); // true - orderby(from smalest), false - orderby descending
    }
}
