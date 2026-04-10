using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FAQManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FaqController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FaqController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Faq
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Faq>>> GetFaqs()
    {
        return await _context.Faqs.ToListAsync();
    }

    // GET: api/Faq/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Faq>> GetFaq(int id)
    {
        var faq = await _context.Faqs.FindAsync(id);

        if (faq == null)
            return NotFound(new { message = $"FAQ with ID {id} not found." });

        return faq;
    }

    // POST: api/Faq
    [HttpPost]
    public async Task<ActionResult<Faq>> CreateFaq([FromBody] Faq faq)
    {
        if (faq == null)
            return BadRequest();

        _context.Faqs.Add(faq);
        await _context.SaveChangesAsync();

        // Returns 201 Created and adds a 'Location' header to the response
        return CreatedAtAction(nameof(GetFaq), new { id = faq.Id }, faq);
    }

    // PUT: api/Faq/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFaq(int id, [FromBody] Faq faq)
    {
        if (id != faq.Id)
            return BadRequest("ID mismatch between URL and body.");

        var existingFaq = await _context.Faqs.FindAsync(id);
        if (existingFaq == null)
            return NotFound();

        // Update properties
        existingFaq.Question = faq.Question;
        existingFaq.Answer = faq.Answer;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FaqExists(id)) return NotFound();
            else throw;
        }

        return NoContent(); // Proper REST response for a successful update with no body
    }

    // DELETE: api/Faq/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFaq(int id)
    {
        var faq = await _context.Faqs.FindAsync(id);
        if (faq == null)
            return NotFound();

        _context.Faqs.Remove(faq);
        await _context.SaveChangesAsync();

        return NoContent(); // Or Ok(new { message = "Deleted" })
    }

    private bool FaqExists(int id)
    {
        return _context.Faqs.Any(e => e.Id == id);
    }
}