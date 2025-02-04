using WebApi_task.Data;
using WebApi_task.Models;
public class CommentDTO
{
    public int Id { get; set; } // Primary Key
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int StockId { get; set; } // Foreign Key => references to the entity of Stock 

    public static CommentDTO FromModel(Comment comment)
    {
        return new CommentDTO
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedDate = comment.CreatedDate
        };
    }
    public Comment ToModel()
    {
        return new Comment
        {
            Id = Id,
            Title = Title,
            Content = Content,
            CreatedDate = CreatedDate
        };
    }
}