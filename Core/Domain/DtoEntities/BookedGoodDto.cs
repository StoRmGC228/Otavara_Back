namespace Domain.DtoEntities;

using Newtonsoft.Json;

public class BookedGoodDto
{
    [JsonIgnore] public Guid UserId { get; set; }
    public Guid GoodId { get; set; }

    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    [JsonIgnore] public DateTime BookingExpirationDate { get; set; }
    public int Count { get; set; }
}