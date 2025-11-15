using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimSystem.Web.Data;
using System.Linq;

using ClaimSystem.Web.ViewModels;

namespace ClaimSystem.Web.Controllers
{
    public class SummaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SummaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Summary
        public IActionResult Index()
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
// Controller prepared for POE Part 3
