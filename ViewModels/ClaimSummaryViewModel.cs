namespace ClaimSystem.Web.ViewModels
{
    public class ClaimSummaryViewModel
    {
        public int ClaimId { get; set; }
        public string ContractorName { get; set; } = "";
        public string ContractName { get; set; } = "";
        public decimal Rate { get; set; }
        public int Hours { get; set; }
        public decimal Total { get; set; }
    }
}
