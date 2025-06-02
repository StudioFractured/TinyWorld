using UnityEngine;

public class IncreaseAttackCollectable : AbstractCollectable
{
    public override void OnCollect(Collider2D _colliderHit)
    {
        if (_colliderHit.TryGetComponent(out PlayerWeaponHandler _weapon))
        {
            _weapon.IncreaseDamage();
            Destroy(gameObject);

            if (_colliderHit.TryGetComponent(out PlayerBuffText _text))
            {
                _text.Play("Atk Up"/*, Color.yellow*/);
            }
        }
    }
}
