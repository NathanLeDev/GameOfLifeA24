using GameOfLifeA24.Cells;
using GameOfLifeA24.Factories;
using GameOfLifeA24.Rules;

namespace GameOfLifeA24;

internal sealed class Grid
{
    private readonly int _rows;
    private readonly int _columns;
    private Cell[,] _cells;
    private readonly ICellFactory _cellFactory;
    private readonly IRule _rule;

    public Grid(int rows, int cols, List<(int x, int y)> initialAliveCells, IRule rule, ICellFactory factory)
    {
        _rows = rows;
        _columns = cols;
        _rule = rule;
        _cellFactory = factory;

        InitializeGrid(initialAliveCells);

    }

    public void InitializeGrid(List<(int x, int y)> initialAliveCells)
    {
        _cells = new Cell[_rows, _columns];

        // full of dead cells by default
        for (int x = 0; x < _rows; x++)
            for (int y = 0; y < _columns; y++)
                _cells[x, y] = _cellFactory.CreateCell(x, y, isAlive: false);

        // cells that are alive based on the input list
        foreach (var (x, y) in initialAliveCells)
            if (x >= 0 && x < _rows && y >= 0 && y < _columns)
                _cells[x, y] = _cellFactory.CreateCell(x, y, isAlive: true);
    }

    public void UpdateGrid()
    {
        // Computation of every cells next state based on the current grid, without modifying it
        Cell[,] newCells = new Cell[_rows, _columns];

        for (int x = 0; x < _rows; x++)
        {
            for (int y = 0; y < _columns; y++)
            {
                int aliveNeighbors = GetAliveNeighborsCount(x, y);
                newCells[x, y] = _rule.GetNextState(_cells[x, y], aliveNeighbors);
            }
        }

        // replace the old grid with the new one
        _cells = newCells;
    }
    public Cell GetCell(int x, int y)
    {
        return _cells[x, y];
    }

    private int GetAliveNeighborsCount(int x, int y)
    {
        int count = 0;

        // dx & dy equal -1, 0 or +1
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                // this loop in order to stay in the limit of the grid
                if (nx >= 0 && nx < _rows && ny >= 0 && ny < _columns)
                    if (_cells[nx, ny].IsAlive())
                        count++;
            }
        }

        return count;
    }
}