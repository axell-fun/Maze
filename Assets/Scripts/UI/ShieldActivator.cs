using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShieldActivator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _maxTimeUseShield;

    private float _currentTimeUseShield;
    private bool _isCanUseShield;
    
    public Action<bool> UseShield;

    private void Update()
    {
        if (_isCanUseShield)
        {
            if (_currentTimeUseShield <_maxTimeUseShield)
                _currentTimeUseShield += Time.deltaTime;
            else
                TakeDownShield();
        }
    }

    private void TakeDownShield()
    {
        UseShield?.Invoke(false);
        _isCanUseShield = false;
        _currentTimeUseShield = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UseShield?.Invoke(true);
        _isCanUseShield = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TakeDownShield();
    }
}
