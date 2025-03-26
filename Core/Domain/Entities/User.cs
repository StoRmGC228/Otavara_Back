namespace Domain.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string HashPassword { get; set; }

    public virtual List<Participant> SubscribedEvents { get; set; }
    public virtual List<RequestedCard> WishedCards { get; set; }
    public virtual List<BookedGood> BookedGoods { get; set; }
    public Guid Id { get; set; }
}