using System.ComponentModel.DataAnnotations;

namespace FAQManagement.Models
{
    public class FaqCategory
    {
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public string CategoryDescription { get; set; } = string.Empty;

        public string? CategoryImage { get; set; }

        [Required]
        public int CategorySequence { get; set; }

        public bool IsParent { get; set; }

        public int? ParentSequence { get; set; }
    }
}
