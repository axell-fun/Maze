using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ShieldActivator _shieldActivator;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _shieldMaterial;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private bool _isCanTakeDamage;
    [SerializeField] private float _timeToRollbackPosition;

    private MeshRenderer _mesh;
    private PlayerMovement _playerMovement;
    private Vector3 _startPosition;
    
    public Action FinishReached;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();

        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _shieldActivator.UseShield += UseShield;
    }

    private void OnDisable()
    {
        _shieldActivator.UseShield -= UseShield;
    }

    private void UseShield(bool shieldActivityStatus)
    {
        if (shieldActivityStatus)
        {
            _mesh.material = _shieldMaterial;
            _isCanTakeDamage = false;
        }
        else
        {
            _mesh.material = _defaultMaterial;
            _isCanTakeDamage = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Finish finish))
        {
            finish.PlayEffect();
            FinishReached?.Invoke();
        }
        
        if (other.TryGetComponent(out DeathZone deathZone) && _isCanTakeDamage)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _playerMovement.StopMove();
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        _mesh.enabled = false;

        StartCoroutine(ResetPosition());
    }

    private void BackToStart()
    {
        _mesh.enabled = true;
        transform.position = _startPosition;
        _playerMovement.MoveToFinish();
    }
    
    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(_timeToRollbackPosition);
        BackToStart();
    }
}