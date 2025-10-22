// Models/SupportingDocument.cs
using System.ComponentModel.DataAnnotations;

namespace ClaimSystem.Web.Models

{
    public class SupportingDocument
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public int ClaimId { get; set; }
        public Claim? Claim { get; set; }

        [Required]
        public string OriginalFileName { get; set; } = "";

        [Required]
        public string StoredFileName { get; set; } = "";

        [Required]
        public string FilePath { get; set; } = "";

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }
}
