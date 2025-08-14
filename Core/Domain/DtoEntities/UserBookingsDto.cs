namespace Domain.DtoEntities;

public class UserBookingsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Count { get; set; }
}