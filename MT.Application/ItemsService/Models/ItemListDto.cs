namespace MT.Application.ItemsService.Models;

public class ItemsListDto
{
    public Guid ItemId { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal? PossiblePrice { get; set; }
    
    public decimal? PriceDelta { get; set; }
    
    public bool? PossibleUseful { get; set; }
    
    public bool Usefull { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime Date { get; set; }
    
    public List<Guid>? TagsId { get; set; }
    
    public Guid User { get; set; }
}