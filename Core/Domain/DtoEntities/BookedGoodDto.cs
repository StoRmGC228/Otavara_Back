using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DtoEntities
{
    public class BookedGoodDto
    {
        public Guid UserId { get; set; }
        public Guid GoodId { get; set; }
        public virtual GoodCreationDto Good { get; set; }
        public virtual DateTime BookingExpirationDate { get; set; }
        public int Count { get; set; }
    }
}
