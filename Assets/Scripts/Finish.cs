using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;

    public void PlayEffect()
    {
        _confetti.Play();
    }
}
