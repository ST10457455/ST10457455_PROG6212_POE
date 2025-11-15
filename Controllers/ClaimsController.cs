using Microsoft.AspNetCore.Mvc;
using ClaimSystem.Web.Data;
using ClaimSystem.Web.Models;
using ClaimSystem.Web.ViewModels;

namespace ClaimSystem.Web.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST ALL CLAIMS
        public IActionResult Index()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.Contracts = _context.Contracts.ToList();
            return View(new MonthlyClaim());
        }

        // CREATE (POST)
        [HttpPost]
        public IActionResult Create(MonthlyClaim claim)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Contracts = _context.Contracts.ToList();
                return View(claim);
            }

            var contract = _context.Contracts
                .FirstOrDefault(c => c.ContractId == claim.ContractId);

            if (contract != null)
            {
                claim.Total = claim.Hours * contract.HourlyRate;
            }

            _context.Claims.Add(claim);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // SUMMARY
        public IActionResult Summary()
        {
            var summaries =
                (from claim in _context.Claims
                 join contract in _context.Contracts
                    on claim.ContractId equals contract.ContractId
                 join contractor in _context.Contractors
                    on contract.ContractorId equals contractor.ContractorId
                 select new ClaimSummaryViewModel
                 {
                     ClaimId = claim.ClaimId,
                     ContractorName = contractor.FullName,
                     ContractName = contract.ContractName,
                     Rate = contract.HourlyRate,
                     Hours = claim.Hours,
                     Total = claim.Total
                 }).ToList();

            return View(summaries);
        }
    }
}
