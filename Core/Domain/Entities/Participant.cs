namespace Domain.Entities;

public class Participant
{
    public virtual string Username { get; set; }
    public virtual string PhotoUrl { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public Guid EventId { get; set; }
    public virtual Event Event { get; set; }
}