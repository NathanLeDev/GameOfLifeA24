using GameOfLifeA24.Cells;
using GameOfLifeA24.Factories;
using GameOfLifeA24.Rules;

namespace GameOfLifeA24;

public sealed class GameOfLife
{
    private readonly Grid _grid;

    public GameOfLife(int rows, int cols, IRule rule, List<(int x, int y)> initialAliveCells)
    {
        // Create the CellFactory and pass it to the Grid, so that the Grid can create cells without knowing the details of how they are created
        ICellFactory factory = new CellFactory();

        _grid = new Grid(rows, cols, initialAliveCells, rule, factory);
    }

    public void Update()
    {
        _grid.UpdateGrid();
    }

    public Cell GetCell(int x, int y)
    {
        return _grid.GetCell(x, y);
    }
}
