using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Rules;

public sealed class StandardRule : IRule
{
    public Cell GetNextState(Cell cell, int aliveNeighbors)
    {
        return cell.NextState(aliveNeighbors);
    }
}
