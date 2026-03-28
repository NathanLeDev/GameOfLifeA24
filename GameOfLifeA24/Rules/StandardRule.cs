using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Rules;

public sealed class StandardRule : IRule
{
    // StandardRule just delegates, all the logic is already contained within AliveCell.NextState and DeadCell.NextState
    public Cell GetNextState(Cell cell, int aliveNeighbors)
    {
        return cell.NextState(aliveNeighbors);
    }
}
