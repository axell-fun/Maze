using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LevelSplash : MonoBehaviour
{
    [SerializeField] private GameObject _splash;
    [SerializeField] private float _alphaChangeTime;
    
    private static bool IsFirstStart = true;
    
    private const int MaxFade = 1;
    private const int MinFade = 0;

    private void Start()
    {
        if (!IsFirstStart)
        {
            _splash.SetActive(true);
            _splash.GetComponent<Image>().DOFade(MinFade, _alphaChangeTime).OnComplete(() =>
            {
                _splash.SetActive(false);
            });
        }

        IsFirstStart = false;
    }

    public void TurnOnSplash(Action fadeChanged)
    {
        _splash.SetActive(true);

        Image splashImage = _splash.GetComponent<Image>();
        splashImage.color = new Color(splashImage.color.r, splashImage.color.g, splashImage.color.b, MinFade);

        splashImage.DOFade(MaxFade, _alphaChangeTime).OnComplete(() =>
        {
            fadeChanged?.Invoke();
        });
    }
}
