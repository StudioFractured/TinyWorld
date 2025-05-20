using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] TagCollectionSO _tags = null;
    [SerializeField] int _damage = 1;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (!_tags.HasTag(_other.gameObject)) return;

        if (_other.TryGetComponent(out HealthBehaviour _health))
        {
            _health.TakeDamage(gameObject, _damage);
        }
    }
}
