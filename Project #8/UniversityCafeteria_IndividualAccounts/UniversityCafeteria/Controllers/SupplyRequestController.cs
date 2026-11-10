using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityCafeteria.Data;
using UniversityCafeteria.Models;

namespace UniversityCafeteria.Controllers;

[Authorize]
public class SupplyRequestController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _users;
    public SupplyRequestController(ApplicationDbContext db, UserManager<ApplicationUser> users)
    {
        _db = db;
        _users = users;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _users.GetUserId(User)!;
        IQueryable<SupplyRequest> query = _db.SupplyRequests.Include(r => r.User);
        if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            query = query.Where(r => r.UserId == userId);
        return View(await query.OrderByDescending(r => r.RequestDate).ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Products = await _db.Products.Where(p => p.Quantity > 0).OrderBy(p => p.Name).ToListAsync();
        return View(new CreateSupplyRequestViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSupplyRequestViewModel model)
    {
        var lines = model.Lines.Where(x => x.ProductId > 0 && x.Quantity > 0).ToList();
        if (!lines.Any()) ModelState.AddModelError("", "Choose at least one product and quantity.");
        var ids = lines.Select(x => x.ProductId).Distinct().ToList();
        var products = await _db.Products.Where(p => ids.Contains(p.Id)).ToDictionaryAsync(p => p.Id);
        foreach (var line in lines)
        {
            if (!products.ContainsKey(line.ProductId))
                ModelState.AddModelError("", "One of the selected products does not exist.");
            else if (line.Quantity > products[line.ProductId].Quantity)
                ModelState.AddModelError("", $"Not enough stock for {products[line.ProductId].Name}.");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Products = await _db.Products.Where(p => p.Quantity > 0).ToListAsync();
            return View(model);
        }

        var request = new SupplyRequest
        {
            UserId = _users.GetUserId(User)!,
            RequestDate = DateTime.UtcNow,
            Status = "Pending"
        };
        foreach (var line in lines)
        {
            var product = products[line.ProductId];
            request.Items.Add(new RequestItem
            {
                ProductId = product.Id,
                Quantity = line.Quantity,
                Price = product.Price
            });
            request.TotalPrice += product.Price * line.Quantity;
        }
        _db.SupplyRequests.Add(request);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id = request.Id });
    }

    public async Task<IActionResult> Details(int id)
    {
        var request = await _db.SupplyRequests
            .Include(r => r.User).Include(r => r.Items).ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (request == null) return NotFound();
        if (!CanView(request)) return Forbid();
        return View(request);
    }

    private bool CanView(SupplyRequest request) =>
        User.IsInRole("Admin") || User.IsInRole("SuperAdmin") ||
        request.UserId == _users.GetUserId(User);

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(int id, string status)
    {
        if (status is not ("Approved" or "Rejected")) return BadRequest();
        var request = await _db.SupplyRequests.FindAsync(id);
        if (request == null) return NotFound();
        request.Status = status;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id });
    }
}
