namespace Domain.DtoEntities;

public class PaginatedGoodsDto
{
    public int TotalPages { get; set; }
    public List<GoodDto> PaginatedEntities { get; set; }
}