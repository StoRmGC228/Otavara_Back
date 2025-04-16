namespace Domain.DtoEntities;

using Entities;

public class PaginatedDto<T> where T : IBaseEntity
{
    public int TotalPages { get; set; }
    public List<T> PaginatedEntities { get; set; }
}