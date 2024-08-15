using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MT.Domain.Entity;

[Table("tags")]
public partial class Tag
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TagId { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }
    
    public ICollection<Item>? Items { get; set; } = new List<Item>();
    
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Tags")]
    public User User { get; set; }
}
