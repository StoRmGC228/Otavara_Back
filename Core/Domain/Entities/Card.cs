namespace Domain.Entities;

public class Card : IBaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageLink { get; set; }
    public string TcgPlayerLink { get; set; }
    public string CardMarketLink { get; set; }
    public string CardHoarderLink { get; set; }
    public string Code { get; set; }
}