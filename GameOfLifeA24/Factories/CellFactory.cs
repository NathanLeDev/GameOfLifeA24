using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Factories;

internal sealed class CellFactory : ICellFactory
{
    // decide what cell to create
    public Cell CreateCell(int x, int y, bool isAlive)
    {
        if (isAlive)
            return new AliveCell(x, y);

        return new DeadCell(x, y);
    }
}
