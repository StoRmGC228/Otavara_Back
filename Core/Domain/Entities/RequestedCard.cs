namespace Domain.Entities;

public class RequestedCard : IBaseEntity
{
    public Guid RequesterId { get; set; }
    public Guid EventId { get; set; }
    public string Link { get; set; }
    public string Code { get; set; }

    public int Number { get; set; }
    public DateOnly RequestedDate { get; set; }
    public virtual Event RequestedEvent { get; set; }
    public virtual User Requester { get; set; }
    public Guid Id { get; set; }
}