using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookedGood
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid GoodId { get; set; }
        public virtual Good Good { get; set; }
    }
}
