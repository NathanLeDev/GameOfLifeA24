using GameOfLifeA24.Cells;

namespace GameOfLifeA24.Rules;

public interface IRule
{
    // This method takes a Cell and the number of alive neighbors, and returns the next state of the cell based on the rules of the game
    Cell GetNextState(Cell cell, int aliveNeighbors);
}
