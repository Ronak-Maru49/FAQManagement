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

        // 1. GET ALL (With Join for Table View)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            var result = await (
                from sub in _context.FaqSubCategories
                join cat in _context.FaqCategories on sub.CategoryId equals cat.Id into catGroup
                from cat in catGroup.DefaultIfEmpty()
                select new
                {
                    id = sub.Id,
                    categoryId = sub.CategoryId,
                    categoryName = cat != null ? cat.CategoryName : "General",
                    subCategoryName = sub.SubCategoryName,
                    subCategoryDescription = sub.SubCategoryDescription,
                    subCategoryImage = sub.SubCategoryImage,
                    subCategorySequence = sub.SubCategorySequence
                }
            ).OrderBy(x => x.subCategorySequence).ToListAsync();

            return Ok(result);
        }

        // 2. GET BY ID (For Edit Form Binding)
        [HttpGet("{id}")]
        public async Task<ActionResult<FaqSubCategory>> GetById(int id)
        {
            var data = await _context.FaqSubCategories.FindAsync(id);
            if (data == null) return NotFound(new { message = "Data not found" });
            return Ok(data);
        }

        // 3. CREATE
        [HttpPost]
        public async Task<ActionResult<FaqSubCategory>> Create(FaqSubCategory subCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.FaqSubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return Ok(new { message = "SubCategory created successfully", data = subCategory });
        }

        // 4. FULL FIXED UPDATE (EDIT)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FaqSubCategory subCategory)
        {
            if (id != subCategory.Id) return BadRequest(new { message = "ID mismatch" });

            var existing = await _context.FaqSubCategories.FindAsync(id);
            if (existing == null) return NotFound(new { message = "SubCategory not found" });

            // Basic Fields Update
            existing.CategoryId = subCategory.CategoryId;
            existing.SubCategoryName = subCategory.SubCategoryName;
            existing.SubCategoryDescription = subCategory.SubCategoryDescription;
            existing.SubCategorySequence = subCategory.SubCategorySequence;

            // Image Logic: Agar nayi image aayi hai tabhi update karein, 
            // warna purani wali hi rehne dein (Overwrite protection)
            if (!string.IsNullOrEmpty(subCategory.SubCategoryImage))
            {
                existing.SubCategoryImage = subCategory.SubCategoryImage;
            }

            try
            {
                _context.Entry(existing).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { message = "SubCategory updated successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.FaqSubCategories.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        // 5. DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.FaqSubCategories.FindAsync(id);
            if (data == null) return NotFound(new { message = "Already deleted or not found" });

            _context.FaqSubCategories.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(new { message = "SubCategory deleted successfully" });
        }
    }
}