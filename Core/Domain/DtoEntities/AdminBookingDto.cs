namespace Domain.DtoEntities;

public class AdminBookingDto
{
    public string GoodName { get; set; }
    public string BookerUsername { get; set; }
    public string BookerFirstName { get; set; }
    public int CountOfBooking { get; set; }
    public DateTime CreatedAt { get; set; }
    public string GoodImage { get; set; }
    public string UserImage { get; set; }
    public Guid GoodId { get; set; }
    public Guid UserId { get; set; }
}