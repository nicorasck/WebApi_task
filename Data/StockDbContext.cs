using System;
using Microsoft.EntityFrameworkCore;
using WebApi_task.Models;

namespace WebApi_task.Data;

public class StockDbContext : DbContext
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) {}


    // This is not necessary when it comes to SQlite.
    // protected override void OnModelCreating(ModelBuilder modelBuilder) // using FLuent API (One-to-Many)
    // {
    //     modelBuilder.Entity<Stock>()
    //     .HasMany(s => s.Comments)          // One-to-Many
    //     .WithOne(c => c.Stock)             // 
    //     .HasForeignKey(a => a.StockId);    // Foreign Key => references to the entity of User
    // }
}