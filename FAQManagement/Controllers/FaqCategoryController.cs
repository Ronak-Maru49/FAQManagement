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

        // GET: api/FaqCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FaqCategory>>> GetAll()
        {
            return await _context.FaqCategories.ToListAsync();
        }

        // GET: api/FaqCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FaqCategory>> GetById(int id)
        {
            var data = await _context.FaqCategories.FindAsync(id);

            if (data == null)
                return NotFound();

            return data;
        }

        // POST: api/FaqCategory
        [HttpPost]
        public async Task<ActionResult<FaqCategory>> Create(FaqCategory category)
        {
            _context.FaqCategories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Category created successfully",
                data = category
            });
        }

        // PUT: api/FaqCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FaqCategory category)
        {
            var existing = await _context.FaqCategories.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.CategoryName = category.CategoryName;
            existing.CategoryDescription = category.CategoryDescription;
            existing.CategoryImage = category.CategoryImage;
            existing.CategorySequence = category.CategorySequence;
            existing.IsParent = category.IsParent;
            existing.ParentSequence = category.ParentSequence;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Category updated successfully" });
        }

        // DELETE: api/FaqCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.FaqCategories.FindAsync(id);

            if (data == null)
                return NotFound();

            _context.FaqCategories.Remove(data);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category deleted successfully" });
        }
    }
}