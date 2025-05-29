using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] float _maxValue = 10f;
    [SerializeField] float _initialValue = 5f;

    [Header("// READONLY")]
    [SerializeField] public float _currentValue = 0f;
    [SerializeField] GameObject _lastDamageSource = null;
    [SerializeField] bool _hasTakenDamageThisFrame = false;
    public bool isPlayer = false;

    public event UnityAction<float> OnHealed = null;
    public event UnityAction<float> OnDamageTaken = null;
    public event UnityAction OnDie = null;

    public GameObject LastDamageSource => _lastDamageSource;
    public bool HasTakenDamageThisFrame => _hasTakenDamageThisFrame;

    [Header("Damage Flash Settings")]
    public Color FlashColor = Color.white;
    public float FlashTime = 0.1f;

    public SpriteRenderer _spriteRenderer;
    public Material _material;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _material = new Material(_spriteRenderer.material);
        _spriteRenderer.material = _material; // ✅ Assign the material back

        ResetValue();
    }

    private void LateUpdate()
    {
        _hasTakenDamageThisFrame = false;
    }

    public void RestoreHealth(float value)
    {
        _currentValue += value;
        _currentValue = Mathf.Clamp(_currentValue, 0f, _maxValue);
        OnHealed?.Invoke(_currentValue);
    }

    public void TakeDamage(GameObject source, float value)
    {
        if (!enabled) return;

        _lastDamageSource = source;
        _currentValue -= value;
        _currentValue = Mathf.Clamp(_currentValue, 0f, _maxValue);

        StartCoroutine(DamageFlash()); // ✅ Correctly start the coroutine

        if (_currentValue <= 0)
        {
            Die();
        }
        else
        {
            if (isPlayer)
            {
                PlayerHealth ph = FindAnyObjectByType<PlayerHealth>();
                ph.ShakeCam();
            }

            _hasTakenDamageThisFrame = true;
            OnDamageTaken?.Invoke(_currentValue);
        }
    }


    public void ResetValue()
    {
        _currentValue = _initialValue;
    }

    public virtual void Die()
    {
        StartCoroutine(DelayedDeath());
    }

    private IEnumerator DelayedDeath()
    {
        yield return new WaitForSeconds(0.15f);
        OnDie?.Invoke();
        gameObject.SetActive(false);
    }

    public void IncreaseMaxValue()
    {
        _maxValue++;
        RestoreHealth(1);
    }

    [ContextMenu("// TakeDamage()")]
    public void TakeDamage()
    {
        TakeDamage(gameObject, 0);
    }

    private IEnumerator DamageFlash()
    {
        SetFlashColor();

        SetFlashAmount(5f);
        yield return new WaitForSeconds(FlashTime);
        SetFlashAmount(0.5f);

        yield break;

        float flashInDuration = 0.1f;
        float holdDuration = 0.25f;
        float flashOutDuration = 0.1f;

        float elapsedTime = 0f;
        while (elapsedTime < flashInDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentFlashAmount = Mathf.Lerp(0.5f, 10.0f, elapsedTime / flashInDuration);
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }

        SetFlashAmount(10.0f);
        yield return new WaitForSeconds(holdDuration);

        elapsedTime = 0f;
        while (elapsedTime < flashOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentFlashAmount = Mathf.Lerp(10.0f, 0.5f, elapsedTime / flashOutDuration);
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }

        SetFlashAmount(0.5f);
    }


    private void SetFlashColor()
    {
        _material.SetColor("_FlashColor", FlashColor);
    }

    private void SetFlashAmount(float amount)
    {
        _material.SetFloat("_FlashAmount", amount);
    }
}
