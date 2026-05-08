using System;
using System.Collections.Generic;

namespace FAQManagement.Models;

public partial class FaqCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryDescription { get; set; } = null!;

    public string? CategoryImage { get; set; }

    public int CategorySequence { get; set; }

    public bool IsParent { get; set; }

    public int? ParentSequence { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<FaqSubCategory> FaqSubCategories { get; set; } = new List<FaqSubCategory>();

    public virtual ICollection<FaqCategory> InverseParent { get; set; } = new List<FaqCategory>();

    public virtual FaqCategory? Parent { get; set; }
}
