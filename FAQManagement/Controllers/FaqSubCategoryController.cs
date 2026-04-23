using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FAQManagement.Models;

namespace FAQManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqSubCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FaqSubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            var result = await (
                from sub in _context.FaqSubCategories
                join cat in _context.FaqCategories on sub.CategoryId equals cat.Id into catGroup
                from cat in catGroup.DefaultIfEmpty()
                select new
                {
                    sub.Id,
                    sub.CategoryId,
                    categoryName = cat != null ? cat.CategoryName : "",
                    sub.SubCategoryName,
                    sub.SubCategoryDescription,
                    sub.SubCategoryImage,
                    sub.SubCategorySequence
                }
            ).ToListAsync();

            return Ok(result);
        }

        [HttpGet("ByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<FaqSubCategory>>> GetByCategory(int categoryId)
        {
            return await _context.FaqSubCategories
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FaqSubCategory>> GetById(int id)
        {
            var data = await _context.FaqSubCategories.FindAsync(id);
            if (data == null) return NotFound();
            return data;
        }

        [HttpPost]
        public async Task<ActionResult<FaqSubCategory>> Create(FaqSubCategory subCategory)
        {
            _context.FaqSubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return Ok(new { message = "SubCategory created successfully", data = subCategory });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FaqSubCategory subCategory)
        {
            var existing = await _context.FaqSubCategories.FindAsync(id);
            if (existing == null) return NotFound();

            existing.CategoryId = subCategory.CategoryId;
            existing.SubCategoryName = subCategory.SubCategoryName;
            existing.SubCategoryDescription = subCategory.SubCategoryDescription;
            existing.SubCategoryImage = subCategory.SubCategoryImage;
            existing.SubCategorySequence = subCategory.SubCategorySequence;

            await _context.SaveChangesAsync();
            return Ok(new { message = "SubCategory updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.FaqSubCategories.FindAsync(id);
            if (data == null) return NotFound();

            _context.FaqSubCategories.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(new { message = "SubCategory deleted successfully" });
        }
    }
}