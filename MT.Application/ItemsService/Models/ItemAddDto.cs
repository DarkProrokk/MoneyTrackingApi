namespace MT.Application.ItemsService.Models;

public class ItemsAddDto
{
    public required string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal? PossiblePrice { get; set; }
    public bool? PossibleUseful { get; set; }
    public bool Usefull { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime Date { get; set; } = DateTime.UtcNow.AddHours(10).Date;
    
    public required List<Guid> TagsId { get; set; }
}