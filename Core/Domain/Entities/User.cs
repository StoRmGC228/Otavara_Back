namespace Domain.Entities;

public class User : BaseEntity
{
    public int TelegramId { get; set; }
    public string TelegramFirstName { get; set; }
    public string TelegramUserName { get; set; }
    public string HashPassword { get; set; }
    public string PhotoURL { get; set; }

    public virtual List<Participant> SubscribedEvents { get; set; }
    public virtual List<RequestedCard> WishedCards { get; set; }
    public virtual List<BookedGood> BookedGoods { get; set; }
    public Guid Id { get; set; }
}