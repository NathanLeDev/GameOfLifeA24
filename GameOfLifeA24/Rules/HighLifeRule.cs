using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Rules;

public class HighLifeRule : IRule
{
    // The HighLife rules are the same as the standard Game of Life, but with an additional condition for a dead cell to become alive
    public Cell GetNextState(Cell cell, int aliveNeighbors)
    {
        if (!cell.IsAlive() && aliveNeighbors == 6)
            return new AliveCell(cell.X, cell.Y);

        return cell.NextState(aliveNeighbors);
    }
}
