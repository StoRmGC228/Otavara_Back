namespace Domain.Entities;

public class Good : IBaseEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int QuantityInStock { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual List<BookedGood> Bookers { get; set; }
}
