using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _wallLeft;
    [SerializeField] private GameObject _wallBottom;
    [SerializeField] private GameObject[] _floor;
    [SerializeField] private Transform _finishPoint;

    public GameObject WallLeft => _wallLeft;
    public GameObject WallBottom => _wallBottom;
    public Transform FinishPoint => _finishPoint;

    public void DisableFloor()
    {
        foreach (var floor in _floor)
        {
            floor.SetActive(false);
        }
    }
}