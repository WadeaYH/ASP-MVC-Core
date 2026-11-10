using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UniversityCafeteria.Models;

public class SupplyRequest
{
    public int Id { get; set; }
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public decimal TotalPrice { get; set; }

    [Required]
    public string Status { get; set; } = "Pending";

    [Required]
    public string UserId { get; set; } = "";
    public ApplicationUser? User { get; set; }

    public List<RequestItem> Items { get; set; } = new();
}
