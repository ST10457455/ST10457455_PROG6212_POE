using Microsoft.AspNetCore.Mvc;
using ClaimSystem.Web.Data;
using ClaimSystem.Web.Models;

namespace ClaimSystem.Web.Controllers
{
    public class ContractorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contractors = _context.Contractors.ToList();
            return View(contractors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Contractor());
        }

        [HttpPost]
        public IActionResult Create(Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _context.Contractors.Add(contractor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractor);
        }
    }
}
