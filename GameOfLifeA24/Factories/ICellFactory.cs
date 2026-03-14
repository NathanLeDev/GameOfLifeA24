using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Factories;

public interface ICellFactory
{
    Cell CreateCell(int x, int y, bool isAlive);
}
