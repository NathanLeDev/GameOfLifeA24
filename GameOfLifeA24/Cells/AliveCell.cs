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
}
