using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoEntities
{
    using System.Text.Json.Serialization;

    public class EventCreationDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public string? Format { get; set; }
        public string Game { get; set; }
        [JsonPropertyName("date")]
        public DateOnly EventDate { get; set; }

        [JsonPropertyName("time")]
        public TimeOnly EventTime { get; set; }
    }
}
