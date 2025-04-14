using Domain.Entities;

namespace Domain.DtoEntities
{
    public class AnnouncementDto
    {
        public int Count { get; set; }
        public DateOnly RequestedDate { get; set; }
        public Guid RequesterId { get; set; }
        public CardDto Card { get; set; }
    }
}
