using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User:BaseEntity
    {
        public string Login { get; set; }
        public string HashPassword { get; set; }

        public virtual List<Participant> SubscribedEvents { get; set; }
        public virtual List<RequestedCard> WishedCards { get;set; }
        public virtual List<BookedGood> BookedGoods { get; set; }
    }
}
