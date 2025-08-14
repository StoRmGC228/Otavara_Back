namespace Domain.Entities;

public class BookedGood
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public Guid GoodId { get; set; }
    public virtual Good Good { get; set; }
    public int Count { get; set; }
    public DateTime BookingExpirationDate { get; set; }
    public DateTime CreatedAt { get; set; }
}