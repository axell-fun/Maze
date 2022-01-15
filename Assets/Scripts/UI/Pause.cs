using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private float _alphaChangeTime;

    private const int MaxFade = 1;
    private const int MinFade = 0;

    private const int MaxTimeScale = 1;
    private const int MinTimeScale = 0;
    
    public void ActivatePause()
    {
        _pausePanel.SetActive(true);
        _pausePanel.GetComponent<Image>().DOFade(MaxFade, _alphaChangeTime).OnComplete(() =>
        {
            Time.timeScale = MinTimeScale;
        });
    }

    public void DisablePause()
    {
        Time.timeScale = MaxTimeScale;
        _pausePanel.GetComponent<Image>().DOFade(MinFade, _alphaChangeTime).OnComplete(() =>
        {
            _pausePanel.SetActive(false);
        });
    }
}