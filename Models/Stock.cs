namespace WebApi_task.Models;

public class Stock
{
    public int Id { get; set; } // Primary Key
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<Comment>? Comments { get; set; } // One Stock can have many comments => One-to-Many (Navigation Property)
}
