using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Rules;

public sealed class StandardRule
{
    Cell GetNextState(cell Cell, int aliveNeighbirs);
}
