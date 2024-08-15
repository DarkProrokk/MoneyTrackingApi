using System.ComponentModel.DataAnnotations;

namespace Tests.Repository.Base;

public class TestEntity
{
    [Key] public Guid Guid { get; set; } = Guid.NewGuid();
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public decimal? Price { get; set; }
}