using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Rules;

public class HighLifeRule : IRule
{
    public Cell GetNextState(Cell cell, int aliveNeighbors)
    {
        // dead cell + 6 neighbours -> new cell
        if (!cell.IsAlive() && aliveNeighbors == 6)
            return new AliveCell(cell.X, cell.Y);

        // else its the standard rule
        return cell.NextState(aliveNeighbors);
    }
}
