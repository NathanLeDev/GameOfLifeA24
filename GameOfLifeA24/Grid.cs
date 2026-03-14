using GameOfLifeA24.Cells;
using GameOfLifeA24.Factories;
using GameOfLifeA24.Rules;

namespace GameOfLifeA24;

internal sealed class Grid
{
    //Game environment where you will find a representation of each cells.
    private int rows;
    private int columns;

    private Cell[,] cells;

    private ICellFactory cellFactory;
    private IRule rule;

    public Grid(int rows, int cols, List<(int x, int y)> initialAliveCells, IRule rule, ICellFactory factory)
    {
        this.rows = rows;
        this.columns = cols;
        this.rule = rule;
        this.cellFactory = factory;

        cells = new Cell[rows, cols];

        InitializeGrid(initialAliveCells);
    }

    public Cell GetCell(int x, int y)
    {
        return cells[x, y];
    }

    private int GetAliveNeighborsCount(int x, int y)
    {
        return rule.GetAliveNeighborsCount(cells, x, y);
    }
}