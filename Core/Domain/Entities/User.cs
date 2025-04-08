namespace Domain.Entities;
public class User : IBaseEntity
{
    public int TelegramId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string PhotoUrl { get; set; }
    public virtual List<Participant> SubscribedEvents { get; set; }
    public virtual List<Announcement> Announcements { get; set; }
    public virtual List<BookedGood> BookedGoods { get; set; }
    public Guid Id { get; set; }
}