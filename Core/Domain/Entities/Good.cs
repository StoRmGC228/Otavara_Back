using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Good:BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<BookedGood> Bookers { get; set; }
    }
}
