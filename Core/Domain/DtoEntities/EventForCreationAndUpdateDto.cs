namespace Domain.DtoEntities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class EventForCreationAndUpdateDto
{
    [FromForm] public string Name { get; set; }
    [FromForm] public IFormFile? Image { get; set; }
    [FromForm] public string? ImageUrl { get; set; }
    [FromForm] public string Description { get; set; }
    [FromForm] public int? Price { get; set; }
    [FromForm] public string? Format { get; set; }
    [FromForm] public DateOnly Date { get; set; }
    [FromForm] public TimeOnly Time { get; set; }
}