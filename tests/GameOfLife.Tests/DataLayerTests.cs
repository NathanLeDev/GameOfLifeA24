using DataLayerGameOfLife;
using DataLayerGameOfLife.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GameOfLife.Tests;

public class DataLayerTests
{
    [Fact]
    public void InitialState_Values_Parses_State()
    {
        var s = new InitialState { State = "1,2;3,2;5,6;" };
        Assert.Contains((1, 2), s.Values);
        Assert.Contains((5, 6), s.Values);
        Assert.Equal(3, s.Values.Count);
    }

    [Fact]
    public void DbContext_Can_Save_And_Read_InitialState_Sqlite_InMemory()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        // SQlite database connection
        var options = new DbContextOptionsBuilder<GameOfLifeContext>()
            .UseSqlite(connection)
            .Options;

        using var ctx = new GameOfLifeContext(options);
        ctx.Database.EnsureCreated();

        ctx.InitialStates.Add(new InitialState { Name = "Test", State = "1,1;" });
        ctx.SaveChanges();

        var loaded = ctx.InitialStates.AsNoTracking().FirstOrDefault(s => s.Name == "Test");
        Assert.NotNull(loaded);
        Assert.Equal("1,1;", loaded!.State);
    }
}
