namespace ClaimSystem.Web.Models
{
    public class Contract
    {
        public int ContractId { get; set; }

        // Foreign key
        public int ContractorId { get; set; }

        public string ContractName { get; set; } = "";

        // The rate for claims
        public decimal HourlyRate { get; set; }

        // Navigation property (important)
        public Contractor? Contractor { get; set; }
    }
}
