using System.ComponentModel.DataAnnotations;

namespace Project5ORM.Models.CodeFirst;

public class Supplier
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = "";

    [MaxLength(30)]
    public string? Phone { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();

    public List<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
