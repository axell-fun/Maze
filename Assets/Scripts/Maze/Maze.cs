using UnityEngine;

public class Maze
{
    private MazeGeneratorCell[,] _cells;

    public int AmountXCells => _cells.GetLength(0);
    public int AmountYCells => _cells.GetLength(1);

    public void SetCells(MazeGeneratorCell[,] cells)
    {
        _cells = cells;
    }

    public MazeGeneratorCell GetCell(int x, int y)
    {
        return _cells[x, y];
    }
}