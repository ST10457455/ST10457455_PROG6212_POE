// Models/Claim.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimSystem.Web.Models

{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public string? LecturerId { get; set; } // link to ApplicationUser.Id

        [Required]
        [Range(0.01, 1000)]
        public decimal HoursWorked { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal HourlyRate { get; set; }

        [NotMapped]
        public decimal TotalAmount => HoursWorked * HourlyRate;

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public string? Notes { get; set; }

        public ICollection<SupportingDocument>? SupportingDocuments { get; set; }
    }
}
