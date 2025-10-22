// Controllers/DashboardController.cs
using ClaimSystem.Web.Data;
using ClaimSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaimSystem.Controllers
{
    [Authorize(Roles = "Coordinator,Manager")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        // GET: /Dashboard/Pending
        public async Task<IActionResult> Pending()
        {
            var pending = await _ctx.Claims
                                   .Include(c => c.SupportingDocuments)
                                   .Where(c => c.Status == ClaimStatus.Pending)
                                   .OrderBy(c => c.SubmissionDate)
                                   .ToListAsync();
            return View(pending);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var claim = await _ctx.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Approved;
            var user = await _userManager.GetUserAsync(User);
            claim.ApprovedBy = user!.Email;
            claim.ApprovedDate = DateTime.UtcNow;

            await _ctx.SaveChangesAsync();

            // Optional: notify via SignalR

            return RedirectToAction(nameof(Pending));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string? reason)
        {
            var claim = await _ctx.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Rejected;
            var user = await _userManager.GetUserAsync(User);
            claim.ApprovedBy = user!.Email;
            claim.ApprovedDate = DateTime.UtcNow;
            claim.Notes = (claim.Notes ?? "") + $"\nRejection note: {reason}";

            await _ctx.SaveChangesAsync();

            // Optional: notify via SignalR

            return RedirectToAction(nameof(Pending));
        }
    }
}
