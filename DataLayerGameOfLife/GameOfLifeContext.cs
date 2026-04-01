using DataLayerGameOfLife.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DataLayerGameOfLife;

public class GameOfLifeContext : DbContext
{
    public DbSet<InitialState> InitialStates { get; set; }

    public GameOfLifeContext(DbContextOptions<GameOfLifeContext> options)
    : base(options) { }

    public GameOfLifeContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=GameOfLife.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InitialState>()
            .HasIndex(s => s.Name)
            .IsUnique();

        // those data are default in the database
        modelBuilder.Entity<InitialState>().HasData(
            new InitialState { Id = 1, Name = "Blinker", State = "1,2;2,2;3,2;" },
            new InitialState { Id = 2, Name = "Block", State = "1,1;1,2;2,1;2,2;" }
        );
    }
}
