using System.ComponentModel.DataAnnotations;

namespace UniversityCafeteria.Models;

public class RequestItem
{
    public int Id { get; set; }
    public int SupplyRequestId { get; set; }
    public SupplyRequest? SupplyRequest { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
