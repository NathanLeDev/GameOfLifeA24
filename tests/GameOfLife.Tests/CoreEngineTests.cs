using DataLayerGameOfLife.Repositories;
using GameOfLifeA24;
using GameOfLifeA24.Rules;
using Xunit;

namespace GameOfLife.Tests;

public class CoreEngineTests
{
    [Fact]
    public void DeadCell_With_Exactly3_Neighbors_Becomes_Alive()
    {
        var alive = new List<(int x, int y)> { (0, 1), (1, 0), (2, 1) };
        var game = new GameOfLifeA24.GameOfLife(4, 4, new StandardRule(), alive);

        game.Update();

        Assert.True(game.GetCell(1, 1).IsAlive());
    }

    [Fact]
    public void HighLifeRule_DeadCell_With_6_Neighbors_Becomes_Alive()
    {
        var alive = new List<(int x, int y)>
    {
        (0,0), (0,1), (0,2),
        (1,0),        (1,2),
        (2,0)
    };
        var game = new GameOfLifeA24.GameOfLife(5, 5, new HighLifeRule(), alive);

        game.Update();

        Assert.True(game.GetCell(1, 1).IsAlive());
    }

    [Fact]
    public void AliveCell_With_2_Neighbors_Survives()
    {
        var alive = new List<(int x, int y)> { (1, 1), (1, 0), (1, 2) };
        var game = new GameOfLifeA24.GameOfLife(4, 4, new StandardRule(), alive);

        game.Update();

        Assert.True(game.GetCell(1, 1).IsAlive());
    }

    [Fact]
    public void AliveCell_With_3_Neighbors_Survives()
    {
        var alive = new List<(int x, int y)> { (1, 1), (1, 0), (1, 2), (0, 1) };
        var game = new GameOfLifeA24.GameOfLife(4, 4, new StandardRule(), alive);

        game.Update();

        Assert.True(game.GetCell(1, 1).IsAlive());
    }

    [Fact]
    public void AliveCell_With_LessThan2_Neighbors_Dies()
    {
        var alive = new List<(int x, int y)> { (1, 1) };
        var game = new GameOfLifeA24.GameOfLife(4, 4, new StandardRule(), alive);

        game.Update();

        Assert.False(game.GetCell(1, 1).IsAlive());
    }

    [Fact]
    public void AliveCell_With_MoreThan3_Neighbors_Dies()
    {
        var alive = new List<(int x, int y)> { (1, 1), (1, 0), (1, 2), (0, 1), (2, 1) };
        var game = new GameOfLifeA24.GameOfLife(4, 4, new StandardRule(), alive);

        game.Update();

        Assert.False(game.GetCell(1, 1).IsAlive());
    }

    [Fact]
    public void RepositoryFactory_Returns_Same_Instance()
    {
        var i1 = RepositoryFactory.Instance;
        var i2 = RepositoryFactory.Instance;
        Assert.Same(i1, i2);
    }

    [Fact]
    public void Update_Is_Simultaneous_Blinker_Flips_Orientation()
    {
        // Horizontal blinker: (1,0),(1,1),(1,2)
        var alive = new List<(int x, int y)> { (1, 0), (1, 1), (1, 2) };
        var game = new GameOfLifeA24.GameOfLife(5, 5, new StandardRule(), alive);

        game.Update();

        // Must become vertical: (0,1),(1,1),(2,1)
        Assert.True(game.GetCell(0, 1).IsAlive());
        Assert.True(game.GetCell(1, 1).IsAlive());
        Assert.True(game.GetCell(2, 1).IsAlive());

        Assert.False(game.GetCell(1, 0).IsAlive());
        Assert.False(game.GetCell(1, 2).IsAlive());
    }
}
