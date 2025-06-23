using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DtoEntities
{
    public class GoodDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TypeOfItem { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
