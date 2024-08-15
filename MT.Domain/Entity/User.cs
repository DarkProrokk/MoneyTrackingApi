using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MT.Domain.Entity;

[Table("users")]
public class User
{
    [Key]
    public Guid Id { get; set; }

    public string? UserName { get; set; }
    
    public string? Email { get; set; }
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
