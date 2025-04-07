using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoEntities
{
    using Entities;

    public class PaginatedDto<T> where T:IBaseEntity
    {
        public int TotalPages { get; set; }
        public List<T> PaginatedEntities { get; set; }

    }
}
