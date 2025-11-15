using Microsoft.AspNetCore.Mvc;
using ClaimSystem.Web.Data;
using ClaimSystem.Web.Models;

namespace ClaimSystem.Web.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contracts = _context.Contracts.ToList();
            return View(contracts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Contractors = _context.Contractors.ToList();
            return View(new Contract());
        }

        [HttpPost]
        public IActionResult Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Contracts.Add(contract);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Contractors = _context.Contractors.ToList();
            return View(contract);
        }
    }
}
