using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimSystem.Web.Models
{
    public class MonthlyClaim
    {
        [Key]
        public int ClaimId { get; set; }

        // Foreign key to Contract
        public int ContractId { get; set; }

        // Hours worked in the month
        public int Hours { get; set; }

        // Total amount calculated for the claim
        public decimal Total { get; set; }

        // Navigation property (optional for Summary, good for Index)
        public Contract? Contract { get; set; }
    }
}
// Model updated for POE submission
