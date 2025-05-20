using System.Collections;
using UnityEngine;

public class ShrinkOnHit : MonoBehaviour
{
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] Transform _target = null;
    [SerializeField] float _shrinkFactor = 0.9f;
    [SerializeField] float _shrinkDuration = 0.2f;

    private void OnEnable()
    {
        _health.OnDamageTaken += _health_OnDamageTaken;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= _health_OnDamageTaken;
    }

    private void _health_OnDamageTaken(float arg0)
    {
        Play();
    }

    private void Play()
    {
        StartCoroutine(Shrink_Routine());
    }

    private IEnumerator Shrink_Routine()
    {
        Vector3 originalScale = _target.localScale;
        Vector3 targetScale = originalScale * _shrinkFactor;

        float halfDuration = _shrinkDuration / 2f;
        float timer = 0f;

        // Shrink phase
        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            _target.localScale = Vector3.Lerp(originalScale, targetScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        _target.localScale = targetScale;

        // Return phase
        timer = 0f;
        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            _target.localScale = Vector3.Lerp(targetScale, originalScale, t);
            timer += Time.deltaTime;
            yield return null;
        }
        _target.localScale = originalScale;
    }
}
