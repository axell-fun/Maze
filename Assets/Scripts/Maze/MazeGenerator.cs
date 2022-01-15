using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 10;

    public Maze GenerateMaze()
    {
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[_width, _height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeGeneratorCell();
                cells[x, y].SetCoordinates(x, y);
            }
        }

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, _height - 1].ChangeStatusWallLeft(false);
            cells[x, _height - 1].ChangeFloorStatus(false);
        }

        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[_width - 1, y].ChangeStatusWallBottom(false);
            cells[_width - 1, y].ChangeFloorStatus(false);
        }

        RemoveWallsWithBacktracker(cells);

        Maze maze = new Maze();
        
        maze.SetCells(cells);
        PlaceMazeExit(cells);

        return maze;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell currentCell = maze[0, 0];
        currentCell.ChangeVisitedStatus(true);
        currentCell.SetDistanceFromStart(0);

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int positionX = currentCell.PositionX;
            int positionY = currentCell.PositionY;

            if (positionX > 0 && !maze[positionX - 1, positionY].IsVisited) 
                unvisitedNeighbours.Add(maze[positionX - 1, positionY]);
            if (positionY > 0 && !maze[positionX, positionY - 1].IsVisited) 
                unvisitedNeighbours.Add(maze[positionX, positionY - 1]);
            if (positionX < _width - 2 && !maze[positionX + 1, positionY].IsVisited) 
                unvisitedNeighbours.Add(maze[positionX + 1, positionY]);
            if (positionY < _height - 2 && !maze[positionX, positionY + 1].IsVisited) 
                unvisitedNeighbours.Add(maze[positionX, positionY + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(currentCell, chosen);

                chosen.ChangeVisitedStatus(true);
                stack.Push(chosen);
                chosen.SetDistanceFromStart(currentCell.DistanceFromStart + 1);
                currentCell = chosen;
            }
            else
            {
                currentCell = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell current, MazeGeneratorCell chosen)
    {
        if (current.PositionX == chosen.PositionX)
        {
            if (current.PositionY > chosen.PositionY) 
                current.ChangeStatusWallBottom(false);
            else 
                chosen.ChangeStatusWallBottom(false);
        }
        else
        {
            if (current.PositionX > chosen.PositionX) 
                current.ChangeStatusWallLeft(false);
            else 
                chosen.ChangeStatusWallLeft(false);
        }
    }
    
    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell finishCell = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, _height - 2].DistanceFromStart > finishCell.DistanceFromStart) 
                finishCell = maze[x, _height - 2];
            if (maze[x, 0].DistanceFromStart > finishCell.DistanceFromStart) 
                finishCell = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[_width - 2, y].DistanceFromStart > finishCell.DistanceFromStart) 
                finishCell = maze[_width - 2, y];
            if (maze[0, y].DistanceFromStart > finishCell.DistanceFromStart)
                finishCell = maze[0, y];
        }
        
        maze[finishCell.PositionX, finishCell.PositionY].SetFinishStatus(true);
    }
}