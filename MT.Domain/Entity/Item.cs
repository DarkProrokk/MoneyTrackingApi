using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace MT.Domain.Entity;

[Table("items")]
[Index("UserId", Name = "IX_Items_UserId")]
public class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ItemId { get; set; }

    [MaxLength(20)]
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? PossiblePrice { get; set; }

    public bool Useful { get; set; }

    public bool? PossibleUseful { get; set; }
    
    [MaxLength(30)]
    public string? Description { get; set; }

    [MaxLength(6)]
    public DateTime Date { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Items")]
    public virtual User User { get; set; } = null!;
    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
}
