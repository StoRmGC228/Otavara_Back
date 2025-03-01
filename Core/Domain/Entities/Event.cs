using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    using Microsoft.VisualBasic;

    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public string? Format  { get; set; }
        public string Game { get; set; }
        public DateTime EventStartTime { get; set; }
        public virtual List<Participant> Participants { get; set; }
        public virtual List<RequestedCard> RequestedCards { get; set; }

    }
}
