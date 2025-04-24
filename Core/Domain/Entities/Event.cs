namespace Domain.Entities;
public class Event : IBaseEntity
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public string? Format { get; set; }
    public string Game { get; set; }
    public DateTime EventStartTime { get; set; }
    public virtual List<Participant>? Participants { get; set; }
    public Guid Id { get; set; }
}