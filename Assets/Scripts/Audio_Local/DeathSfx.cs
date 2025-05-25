using UnityEngine;

public class DeathSfx : AudioPlayer
{
    [SerializeField] HealthBehaviour _health = null;

    private void OnValidate()
    {
        _health = GetComponent<HealthBehaviour>();
    }

    private void OnEnable()
    {
        _health.OnDie += PlayAudio;
    }

    private void OnDisable()
    {
        _health.OnDie -= PlayAudio;
    }

    private void PlayAudio()
    {
        Play();
    }
}
