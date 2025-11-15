namespace ClaimSystem.Web.Models
{
    public class Contractor
    {
        public int ContractorId { get; set; }

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        // This is used in the Summary page (perfect)
        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; } = "";

        // REQUIRED for EF Core relationship mapping
        // One Contractor → Many Contracts
        public List<Contract>? Contracts { get; set; }
    }
}
