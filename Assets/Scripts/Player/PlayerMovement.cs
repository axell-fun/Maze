using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _timeBeforeStartingMove;
    
    private NavMeshAgent _navMeshAgent;
    private Vector3 _finishCell;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(StartGameTimer());
    }

    private IEnumerator StartGameTimer()
    {
        yield return new WaitForSeconds(_timeBeforeStartingMove);
        MoveToFinish();
    }

    public void SetFinishPosition(Vector3 finishCell)
    {
        _finishCell = finishCell;
    }

    public void StopMove()
    {
        _navMeshAgent.enabled = false;
    }
    
    public void MoveToFinish()
    {
        if (!_navMeshAgent.enabled)
        {
            _navMeshAgent.enabled = true;
        }
        
        _navMeshAgent.SetDestination(_finishCell);
    }
}
