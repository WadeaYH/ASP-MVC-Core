using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UniversityCafeteria.DTO;

namespace UniversityCafeteria.Controllers
{
    public class SupplyRequestController : Controller
    {
        private static List<ConfirmedRequest> confirmedRequests = new List<ConfirmedRequest>();

        private List<CreateSupplyRequest> GetDraft()
        {
            string? json = HttpContext.Session.GetString("Draft");
            if (json == null) return new List<CreateSupplyRequest>();
            return JsonSerializer.Deserialize<List<CreateSupplyRequest>>(json)!;
        }

        private void SaveDraft(List<CreateSupplyRequest> items)
        {
            HttpContext.Session.SetString("Draft", JsonSerializer.Serialize(items));
        }

        private ReviewViewModel BuildReview(List<CreateSupplyRequest> items)
        {
            var review = new ReviewViewModel();
            review.Items = items;

            string priority = "Normal";
            foreach (var item in items)
            {
                item.Total = item.Quantity * item.UnitPrice;
                review.Subtotal += item.Total;

                if (item.Priority == "Emergency") priority = "Emergency";
                else if (item.Priority == "Urgent" && priority == "Normal") priority = "Urgent";
            }
            review.Priority = priority;

            switch (priority)
            {
                case "Normal": review.PreparationFee = 2; break;
                case "Urgent": review.PreparationFee = 5; break;
                case "Emergency": review.PreparationFee = 10; break;
            }

            if (review.Subtotal > 200) review.Discount = review.Subtotal * 0.15;
            else if (review.Subtotal > 100) review.Discount = review.Subtotal * 0.10;
            else review.Discount = 0;

            review.FinalTotal = review.Subtotal + review.PreparationFee - review.Discount;
            return review;
        }

        public IActionResult Index()
        {
            ViewBag.Preferred = Request.Cookies["PreferredPriority"];
            return View(GetDraft());
        }

        [HttpPost]
        public IActionResult AddItem(CreateSupplyRequest item)
        {
            if (string.IsNullOrWhiteSpace(item.ProductName) || item.Quantity <= 0 || item.UnitPrice <= 0)
            {
                TempData["Error"] = "Fill the name, and quantity and price must be above zero.";
                return RedirectToAction("Index");
            }

            var items = GetDraft();
            items.Add(item);
            SaveDraft(items);

            TempData["Message"] = item.ProductName + " was added to the request.";
            return RedirectToAction("Index");
        }

        public IActionResult Review()
        {
            return View(BuildReview(GetDraft()));
        }

        [HttpPost]
        public IActionResult ConfirmRequest()
        {
            var items = GetDraft();
            if (items.Count == 0)
            {
                TempData["Error"] = "You cannot confirm an empty request.";
                return RedirectToAction("Index");
            }

            var review = BuildReview(items);

            var confirmed = new ConfirmedRequest();
            confirmed.Id = confirmedRequests.Count + 1;
            confirmed.NumberOfProducts = items.Count;
            confirmed.Subtotal = review.Subtotal;
            confirmed.PreparationFee = review.PreparationFee;
            confirmed.Discount = review.Discount;
            confirmed.FinalTotal = review.FinalTotal;
            confirmed.Status = "Confirmed";
            confirmedRequests.Add(confirmed);

            HttpContext.Session.Remove("Draft");

            TempData["Message"] = "Request #" + confirmed.Id + " was confirmed.";
            return RedirectToAction("MyRequests");
        }

        [HttpPost]
        public IActionResult ClearDraft()
        {
            HttpContext.Session.Remove("Draft");
            TempData["Message"] = "The draft was cleared.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SavePreference(string priority)
        {
            if (priority != "Normal" && priority != "Urgent" && priority != "Emergency")
            {
                TempData["Error"] = "Choose a valid priority.";
                return RedirectToAction("Index");
            }

            var options = new CookieOptions();
            options.Expires = DateTimeOffset.UtcNow.AddDays(7);
            Response.Cookies.Append("PreferredPriority", priority, options);

            return RedirectToAction("Index");
        }

        public IActionResult MyRequests()
        {
            return View(confirmedRequests);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
