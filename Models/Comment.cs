namespace WebApi_task.Models;

public class Comment
{
    public int Id { get; set; } // Primary Key
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public Stock Stock { get; set; } // One-to-Many Navigation Property
    public int StockId { get; set; } // Foreign Key => references to the entity of Stock 
}