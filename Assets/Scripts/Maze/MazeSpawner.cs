using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _finishPrefab;
    [SerializeField] private GameObject _deadZonePrefab;
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Vector3 _cellSize;
    [SerializeField] private Transform _mazeContainer;
    [SerializeField, Range(0, 100)] private int _deadZoneSpawnChance;

    private Maze _maze;
    private Vector3 _finishPoint;

    private const int MinPercentage = 0;
    private const int MaxPercentage = 100;

    public Vector3 FinishPoint => _finishPoint;

    private void Awake()
    {
        MazeGenerator generator = new MazeGenerator();
        _maze = generator.GenerateMaze();

        SpawnMaze();
    }

    private void SpawnMaze()
    {
        for (int x = 0; x < _maze.AmountXCells; x++)
        {
            for (int y = 0; y < _maze.AmountYCells; y++)
            {
                Cell newCell = Instantiate(_cellPrefab, new Vector3(x * _cellSize.x, y * _cellSize.y, y * _cellSize.z), Quaternion.identity, _mazeContainer);

                newCell.WallLeft.SetActive(_maze.GetCell(x, y).IsWallLeft);
                newCell.WallBottom.SetActive(_maze.GetCell(x, y).IsWallBottom);

                if (!_maze.GetCell(x, y).IsFloor)
                {
                    newCell.DisableFloor();
                }

                if (_maze.GetCell(x,y).IsFinish)
                {
                    _finishPoint = newCell.FinishPoint.position;
                    Instantiate(_finishPrefab, _finishPoint, Quaternion.identity, newCell.FinishPoint);
                }
                else
                {
                    if (x == 0 && y == 0)
                        continue;

                    if (Random.Range(MinPercentage, MaxPercentage) < _deadZoneSpawnChance && _maze.GetCell(x, y).IsFloor)
                        Instantiate(_deadZonePrefab, newCell.FinishPoint.position, Quaternion.identity, newCell.FinishPoint);
                }
            }
        }
    }
}