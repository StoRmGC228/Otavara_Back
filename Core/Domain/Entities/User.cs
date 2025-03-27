namespace Domain.Entities;

public class User : BaseEntity
{
    public int TelegramId { get; set; }
    public string? First_name { get; set; }
    public string? Last_name { get; set; }
    public string? Username { get; set; }
    public string Photo_url { get; set; }

    public virtual List<Participant> SubscribedEvents { get; set; }
    public virtual List<RequestedCard> WishedCards { get; set; }
    public virtual List<BookedGood> BookedGoods { get; set; }
    public Guid Id { get; set; }
}