using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Pronia.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Pronia.Models;

public class Product : BaseEntity
{
    [Required]
    public string Name { get; set; }
    [Required]
    [Precision(10,2)]
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? SKU { get; set; }
    public Category Category { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public string MainImageUrl { get; set; }
    [Required]
    public string HoverImageUrl { get; set; }

}

