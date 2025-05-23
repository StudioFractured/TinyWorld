using System;
using System.Threading.Tasks;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] TagCollectionSO _tags = null;
    [SerializeField] int _damage = 1;
    [SerializeField] bool _destroyOnHit = false;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (!_tags.HasTag(_other.gameObject)) return;

        if (_other.TryGetComponent(out HealthBehaviour _health))
        {
            _health.TakeDamage(gameObject, _damage);
        }

        if (_other.TryGetComponent<IReactOnHit>(out var reactable))
        {
            reactable.ReactToHit();
        }

        if (_destroyOnHit)
        {
            var _task = DestroyAsync();
            gameObject.SetActive(false);
        }
    }

    private async Task DestroyAsync()
    {
        await Task.Delay(1000);
        Destroy(gameObject);
    }

    public void IncreaseDamage()
    {
        _damage++;
    }
}
