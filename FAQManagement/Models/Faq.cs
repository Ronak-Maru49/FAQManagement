using System;
using System.Collections.Generic;

namespace FAQManagement.Models;

public partial class Faq
{
    public int Id { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public int DisplayOrder { get; set; }

    public int FaqSubCategoryId { get; set; }

    public int Sequence { get; set; }
}
