namespace GameOfLifeA24.Cells;

internal sealed class AliveCell : Cell
{
   public AliveCell(int x, int y) : base(x, y)
   {
    }

    public override bool IsAlive()
    {
        return true;
    }

    // The rules for a Dead cell are: A live cell with fewer than two live neighbors dies. | A live cell with more than three live neighbors dies. 
    public override Cell NextState(int aliveNeighbors)
    {
        if (aliveNeighbors < 2 || aliveNeighbors > 3)
            return new DeadCell(X, Y);

        return this;
    }
}