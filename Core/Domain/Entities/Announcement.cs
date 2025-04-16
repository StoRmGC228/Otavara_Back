namespace Domain.Entities
{
    public class Announcement : IBaseEntity
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public DateOnly RequestedDate { get; set; }
        public Guid RequesterId { get; set; }
        public Guid CardId { get; set; }
        public virtual User Requester { get; set; }
        public virtual Card Card { get; set; }
    }
}
