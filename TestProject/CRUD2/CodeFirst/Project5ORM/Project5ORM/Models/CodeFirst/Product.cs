using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project5ORM.Models.CodeFirst;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 1000000)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public int SupplierId { get; set; }

    public Supplier? Supplier { get; set; }

    public List<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
}
