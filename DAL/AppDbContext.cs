﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>().HasData(
            new Log
            {
                Id = 1,
                Message = "Hello",
                Date = DateTime.Now.ToString()
            },
            new Log
            {
                Id = 2,
                Message = "World",
                Date = DateTime.Now.ToString()
            }
        );
    }
}