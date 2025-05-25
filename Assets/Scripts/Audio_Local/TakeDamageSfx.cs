using UnityEngine;

public class TakeDamageSfx : AudioPlayer
{
    [SerializeField] HealthBehaviour _health = null;

    private void OnValidate()
    {
        _health = GetComponent<HealthBehaviour>();
    }

    private void OnEnable()
    {
        _health.OnDamageTaken += Play;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= Play;
    }

    private void Play(float arg0)
    {
        Play();
    }
}
