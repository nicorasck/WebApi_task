// using System;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.EntityFrameworkCore;
// using WebApi_task.Data;

// public class StockDbContextFactory : IDesignTimeDbContextFactory<StockDbContext>
// {
//     public UserDbContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
//         optionsBuilder.UseSqlite("Data Source=WebApi_Task.db"); // Connection for Database

//         return new StockDbContext(optionsBuilder.Options);
//     }
// }