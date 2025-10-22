using Microsoft.AspNetCore.Mvc;
using ClaimSystem.Data;
using ClaimSystem.Web.Models;


namespace ClaimSystem.Web.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show the claim submission form
        public IActionResult Create()
        {
            return View();
        }

        // Handle form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.SubmissionDate = DateTime.Now;
                claim.Status = ClaimStatus.Pending;
                _context.Claims.Add(claim);
                _context.SaveChanges();
                return RedirectToAction(nameof(Confirmation));
            }
            return View(claim);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        // Show all pending claims
        public IActionResult Review()
        {       
            var pendingClaims = _context.Claims
                .Where(c => c.Status == ClaimStatus.Pending)
                .ToList();
        return View(pendingClaims);
        }

        // Approve
        [HttpPost]
        public IActionResult Approve(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved;
                claim.ApprovedDate = DateTime.Now;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Review));
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(int claimId, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var document = new SupportingDocument
                {
                    ClaimId = claimId,
                    OriginalFileName = file.FileName,
                    StoredFileName = file.FileName,
                    FilePath = filePath,
                    UploadDate = DateTime.Now
                };

                _context.SupportingDocuments.Add(document);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Review));
        }


        // Reject
        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected;
                claim.ApprovedDate = DateTime.Now;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Review));
        }

    }
}
