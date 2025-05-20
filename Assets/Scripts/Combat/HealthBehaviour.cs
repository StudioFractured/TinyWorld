using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] float _maxValue = 10f;
    [SerializeField] float _initialValue = 5f;

    [Header("// READONLY")]
    [SerializeField] float _currentValue = 0f;
    [SerializeField] GameObject _lastDamageSource = null;

    public event UnityAction<float> OnHealed = null;
    public event UnityAction<float> OnDamageTaken = null;
    public event UnityAction OnDie = null;

    public GameObject LastDamageSource { get => _lastDamageSource; }

    private void Awake()
    {
        ResetValue();
    }

    public void RestoreHealth(float _value)
    {
        _currentValue += _value;
        _currentValue = Mathf.Clamp(_currentValue, 0f, _maxValue);
        OnHealed?.Invoke(_currentValue);
    }

    public void TakeDamage(GameObject _source, float _value)
    {
        _lastDamageSource = _source;
        _currentValue -= _value;
        _currentValue = Mathf.Clamp(_currentValue, 0f, _maxValue);

        if (_currentValue <= 0)
        {
            Die();
        }
        else
        {
            OnDamageTaken?.Invoke(_currentValue);
        }
    }

    public void ResetValue()
    {
        _currentValue = _initialValue;
    }

    public virtual void Die()
    {
        OnDie?.Invoke();
        gameObject.SetActive(false);
    }
}
