public class MazeGeneratorCell
{
    private int _positionX;
    private int _positionY;
    private int _distanceFromStart;

    private bool _isWallLeft = true;
    private bool _isWallBottom = true;
    private bool _isFloor = true;
    private bool _isVisited;
    private bool _isFinish;

    public int PositionX => _positionX;
    public int PositionY => _positionY;
    public int DistanceFromStart => _distanceFromStart;
    public bool IsWallLeft => _isWallLeft;
    public bool IsWallBottom => _isWallBottom;
    public bool IsFloor => _isFloor;
    public bool IsVisited => _isVisited;
    public bool IsFinish => _isFinish;
    
    public void SetCoordinates(int positionX, int positionY)
    {
        _positionX = positionX;
        _positionY = positionY;
    }

    public void ChangeStatusWallLeft(bool activeStatus)
    {
        _isWallLeft = activeStatus;
    }

    public void ChangeStatusWallBottom(bool activeStatus)
    {
        _isWallBottom = activeStatus;
    }

    public void ChangeFloorStatus(bool activeStatuc)
    {
        _isFloor = activeStatuc;
    }
    
    public void ChangeVisitedStatus(bool visitedStatus)
    {
        _isVisited = visitedStatus;
    }

    public void SetFinishStatus(bool finishStatus)
    {
        _isFinish = finishStatus;
    }
    
    public void SetDistanceFromStart(int distance)
    {
        _distanceFromStart = distance;
    }
}