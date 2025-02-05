using Microsoft.AspNetCore.Mvc;
using WebApi_task.Data;
using Microsoft.EntityFrameworkCore;
using WebApi_task.DTO;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly StockDbContext _context;

    public CommentController(StockDbContext context)
    {
        _context = context;
    }

    // GET: api/comment
    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var comments = await _context.Comments.ToListAsync();
        return Ok(comments);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromQuery] int stockId)
    {
        var comment = _context.Comments
        .Where(c => c.Id == stockId)
        .FirstOrDefault();

        if (comment is null)
        {
            return NotFound();
        }
        return Ok(CommentDTO.FromModel(comment));
    }

    // POST: api/comment
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommentDTO commentDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Ensure the referenced StockId exists
        var stockExists = await _context.Stocks.AnyAsync(s => s.Id == commentDTO.StockId);
        if (!stockExists)
        {
            return BadRequest("The specified StockId does not exist.");
        }

        var comment = commentDTO.ToModel();
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, CommentDTO.FromModel(comment));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CommentDTO commentDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comment = await _context.Comments.FindAsync(id);
        if (comment is null)
        {
            return NotFound();
        }

        // Update properties
        comment.Title = commentDTO.Title;
        comment.Content = commentDTO.Content;

        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();

        return Ok(CommentDTO.FromModel(comment));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment is null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
