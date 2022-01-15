using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    
    private void LateUpdate()
    {
        if (!_target)
            return;

        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _speed);
    }
}
