using Microsoft.EntityFrameworkCore;
using WebApi_task.Models;

namespace WebApi_task.Data;

public class StockDbContext : DbContext
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) {}


    //This is not necessary when it comes to SQlite.
    // protected override void OnModelCreating(ModelBuilder modelBuilder) // using FLuent API (One-to-Many)
    // {
    //     modelBuilder.Entity<Comment>()
    //         .HasOne(c => c.Stock)
    //         .WithMany(s => s.Comments)
    //         .HasForeignKey(c => c.StockId)
    //         .OnDelete(DeleteBehavior.Cascade); 
    // }
}