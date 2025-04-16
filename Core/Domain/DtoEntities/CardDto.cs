using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoEntities
{
    public class CardDto
    {
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string TcgPlayerLink { get; set; }
        public string CardMarketLink { get; set; }
        public string CardHoarderLink { get; set; }
        public string Code { get; set; }
    }
}
