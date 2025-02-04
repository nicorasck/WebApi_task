using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_task.Data;
using WebApi_task.DTO;

namespace WebApi_task.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockDbContext _context;
        public StockController(StockDbContext context)
        {
            context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks([FromQuery] bool inlcude = false)
        {
            var stocks = _context.Stocks.AsQueryable();

            if (!inlcude)
            {
                stocks = stocks.Include(s => s.Comments);
                return NotFound();
            }
            var stocklist = await stocks.ToListAsync();
            return Ok(stocklist);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery] int id, bool inlcude = false)
        {
            var stocks = _context.Stocks.AsQueryable();

            if (!inlcude)
            {
                stocks = stocks.Include(s => s.Comments);
                return NotFound();
            }
            var result = await stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (result is null)
            {
                return NotFound();
            }
            return Ok(StockDTO.FromModel(result));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockDTO stockDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = stockDTO.ToModel();
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = stock.Id}, StockDTO.FromModel(stock));
        }
    }
}