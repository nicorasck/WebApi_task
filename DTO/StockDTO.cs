using WebApi_task.Models;

namespace WebApi_task.DTO;

public class StockDTO
{
    public int Id { get; set; } // Primary Key
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public static StockDTO FromModel(Stock stock)
    {
        return new StockDTO
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Price = stock.Price
        };
    }
    public Stock ToModel()
    {
        return new Stock
        {
            Id = Id,
            Symbol = Symbol,
            CompanyName = CompanyName,
            Price = Price
        };
    }
}
