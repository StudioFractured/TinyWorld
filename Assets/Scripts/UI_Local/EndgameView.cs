using System.Collections;
using UnityEngine;

public class EndgameView : CanvasView
{
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] float _delay = 2f;

    private void Awake()
    {
        InstantHide();
    }

    private void OnEnable()
    {
        _health.OnDie += _health_OnDie;
    }

    private void OnDisable()
    {
        _health.OnDie -= _health_OnDie;
    }

    private void _health_OnDie()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(_delay);
        FindFirstObjectByType<AudioHandler>().StopMusic();
        Show();
    }
}
