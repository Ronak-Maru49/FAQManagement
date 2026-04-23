using System.ComponentModel.DataAnnotations;

namespace FAQManagement.Models
{
    public class FaqSubCategory
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string SubCategoryName { get; set; } = string.Empty;

        [Required]
        public string SubCategoryDescription { get; set; } = string.Empty;

        public string? SubCategoryImage { get; set; }

        [Required]
        public int SubCategorySequence { get; set; }
    }
}