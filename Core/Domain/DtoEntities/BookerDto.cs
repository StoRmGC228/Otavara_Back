namespace Domain.DtoEntities;

public class BookerDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string PhotoUrl { get; set; }
    public int Count { get; set; }
}