namespace Domain.DtoEntities
{
    public class BookedGoodDto
    {
        public Guid UserId { get; set; }
        public Guid GoodId { get; set; }
        public GoodAdminDto? Good { get; set; }
        public DateTime BookingExpirationDate { get; set; }
        public int Count { get; set; }
    }
}
