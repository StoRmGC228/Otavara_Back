using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Good : IBaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string TypeOfItem { get; set; }
    public double Price { get; set; }
    public int QuantityInStock { get; set; }
    [JsonIgnore] public DateTime CreatedAt { get; set; }
    public virtual List<BookedGood>? Bookings { get; set; }
    [JsonIgnore] public Guid Id { get; set; }
}