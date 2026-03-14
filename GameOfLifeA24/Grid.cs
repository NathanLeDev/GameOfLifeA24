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

    public Grid(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;

        cells = new Cell[rows, columns];
    }

    public Cell GetCell(int x, int y)
    {
        return cells[x, y];
    }
}