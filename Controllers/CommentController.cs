using Microsoft.AspNetCore.Mvc;
using WebApi_task.Data;
using WebApi_task.Controllers;
using Microsoft.EntityFrameworkCore;

[Route("api/comment")]
    [ApiController]
public class CommentController : ControllerBase
{
    private readonly StockDbContext _context;
    public CommentController(StockDbContext context)
    {
        context = _context;
    }

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommentDTO commentDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = commentDTO.ToModel();
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new {id = comment.Id}, CommentDTO.FromModel(comment));
    }

}