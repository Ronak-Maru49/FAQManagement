using System;
using System.Collections.Generic;

namespace FAQManagement.Models;

public partial class FaqSubCategory
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public string SubCategoryDescription { get; set; } = null!;

    public string? SubCategoryImage { get; set; }

    public int SubCategorySequence { get; set; }

    public string? Questions { get; set; }

    public string? Ans { get; set; }
}
