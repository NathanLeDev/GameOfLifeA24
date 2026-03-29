using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Rules;

public class HighLifeRule : IRule
{
    public Cell GetNextState(Cell cell, int aliveNeighbors)
    {
        if (!cell.IsAlive() && aliveNeighbors == 6)
            return new AliveCell(cell.X, cell.Y);

        return cell.NextState(aliveNeighbors);
    }
}
