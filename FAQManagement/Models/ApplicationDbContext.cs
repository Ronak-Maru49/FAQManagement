using Microsoft.EntityFrameworkCore;
using FAQManagement.Models; // for Faq model

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Faq> Faqs { get; set; }
    public DbSet<FaqCategory> FaqCategories { get; set; }
}