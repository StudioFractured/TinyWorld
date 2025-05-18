using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] float _maxValue = 10f;
    [SerializeField] float _initialValue = 5f;

    [Header("// READONLY")]
    [SerializeField] float _currentValue = 0f;

    public event UnityAction<float> OnDamageTaken = null;

    private void Awake()
    {
        ResetValue();
    }

    public void TakeDamage(float _value)
    {
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
        gameObject.SetActive(false);
    }
}
