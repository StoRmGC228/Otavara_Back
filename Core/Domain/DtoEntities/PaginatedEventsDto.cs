using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoEntities
{
    public class PaginatedEventsDto
    {
        public int TotalPages { get; set; }
        public List<EventCreationDto> PaginatedEntities { get; set; }
    }
}
