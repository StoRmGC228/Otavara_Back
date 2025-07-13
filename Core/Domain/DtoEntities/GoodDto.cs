namespace Domain.DtoEntities;

public class GoodDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TypeOfItem { get; set; }
    public double Price { get; set; }
    public int QuantityInStock { get; set; }
    public string Image { get; set; }
}