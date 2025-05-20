using System;
using UnityEngine;
using UnityEngine.UI;

public class CauseDamageButton : MonoBehaviour
{
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] Button _button = null;

    private void OnEnable()
    {
        _button.onClick.AddListener(Damage);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void Damage()
    {
        _health.TakeDamage();
    }
}
