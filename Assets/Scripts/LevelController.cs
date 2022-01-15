using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshSurface))]
public class LevelController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private LevelSplash _levelSplash;
    [SerializeField] private int _indexGameScene;
    
    private NavMeshSurface _surface;

    private void OnEnable()
    {
        _playerController.FinishReached += EndGame;
    }

    private void Start()
    {
        _surface = GetComponent<NavMeshSurface>();
        
        _surface.BuildNavMesh();
        _playerMovement.SetFinishPosition(_mazeSpawner.FinishPoint);
    }

    private void OnDisable()
    {
        _playerController.FinishReached -= EndGame;
    }

    private void EndGame()
    {
        _levelSplash.TurnOnSplash(() =>
        {
            SceneManager.LoadScene(_indexGameScene);
        });
    }
}