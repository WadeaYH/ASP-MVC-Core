using System.ComponentModel.DataAnnotations;

namespace Project5ORM.Models.CodeFirst;

public class PurchaseOrder
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    public int SupplierId { get; set; }

    public Supplier? Supplier { get; set; }

    public List<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
}
