using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebApi_task.Data;
using WebApi_task.DTO;
using WebApi_task.Models;

namespace WebApi_task.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockDbContext _context;
        public StockController(StockDbContext context)
        {
            _context = context;
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
            var test = _context.Stocks.AsQueryable();

            if (inlcude)
            {
                test = test.Include(s => s.Comments);
            }

            var stock = await test.FirstOrDefaultAsync(s => s.Id == id);

            if (stock is null)
            {
                return NotFound();
            }
            return Ok(StockDTO.FromModel(stock));
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

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, StockDTO.FromModel(stock));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] StockDTO stockDTO, int id, [FromQuery] bool include = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = _context.Stocks.AsQueryable();

            if (include)
            {
                query = query.Include(s => s.Id);
            }

            var stock = await query.FirstOrDefaultAsync(s => s.Id == id);

            if (stock is null)
            {
                return NotFound();
            }

            // Update properties
            stock.Symbol = stockDTO.Symbol;
            stock.CompanyName = stockDTO.CompanyName;
            stock.Price = stockDTO.Price;

            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return Ok(StockDTO.FromModel(stock));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock is null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}