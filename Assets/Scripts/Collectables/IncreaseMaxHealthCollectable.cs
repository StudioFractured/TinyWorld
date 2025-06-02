using UnityEngine;

public class IncreaseMaxHealthCollectable : AbstractCollectable
{
    public override void OnCollect(Collider2D _colliderHit)
    {
        if (_colliderHit.TryGetComponent(out HealthBehaviour _health))
        {
            _health.IncreaseMaxValue();
            Destroy(gameObject);

            if (_colliderHit.TryGetComponent(out PlayerBuffText _text))
            {
                _text.Play("HP Up"/*, Color.red*/);
            }
        }
    }
}
