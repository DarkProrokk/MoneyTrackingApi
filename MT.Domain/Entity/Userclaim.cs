using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MT.Domain.Entity;

[Table("userclaims")]
public partial class Userclaim
{
    [Key]
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
