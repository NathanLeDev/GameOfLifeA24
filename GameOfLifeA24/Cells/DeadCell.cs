namespace GameOfLifeA24.Cells;

internal sealed class DeadCell : Cell
{
    public DeadCell(int x, int y) : base(x, y)
    {
    }
    public override bool IsAlive()
    {
        return false;
    }

    // The rules for a dead cell is: A dead cell with exactly three live neighbors becomes a live cell
    public override Cell NextState(int aliveNeighbors)
    {
        if (aliveNeighbors == 3)
            return new AliveCell(X, Y);

        return this;
    }
}