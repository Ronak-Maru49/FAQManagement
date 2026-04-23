using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FAQManagement.Models;

namespace FAQManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FaqCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FaqCategory>>> GetAll()
        {
            return await _context.FaqCategories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FaqCategory>> GetById(int id)
        {
            var data = await _context.FaqCategories.FindAsync(id);
            if (data == null) return NotFound();
            return data;
        }

        [HttpPost]
        public async Task<ActionResult<FaqCategory>> Create(FaqCategory category)
        {
            _context.FaqCategories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Category created successfully", data = category });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FaqCategory category)
        {
            var existing = await _context.FaqCategories.FindAsync(id);
            if (existing == null) return NotFound();

            existing.CategoryName = category.CategoryName;
            existing.CategoryDescription = category.CategoryDescription;
            existing.CategoryImage = category.CategoryImage;
            existing.CategorySequence = category.CategorySequence;
            existing.IsParent = category.IsParent;
            existing.ParentSequence = category.ParentSequence;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Category updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.FaqCategories.FindAsync(id);
            if (data == null) return NotFound();

            _context.FaqCategories.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Category deleted successfully" });
        }
    }
}