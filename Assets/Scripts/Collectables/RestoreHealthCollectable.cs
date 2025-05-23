using UnityEngine;

public class RestoreHealthCollectable : AbstractCollectable
{
    [SerializeField] float _restoreValue = 1f;

    public override void OnCollect(Collider2D _colliderHit)
    {
        if (_colliderHit.TryGetComponent(out HealthBehaviour _health))
        {
            _health.RestoreHealth(_restoreValue);
            Destroy(gameObject);
        }
    }
}
