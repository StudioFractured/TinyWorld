using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] Collider2D _collider = null;
    [SerializeField] AudioSO _sfx = null;
    [SerializeField] float _time = 0.5f;

    [Header("// PLAYER")]
    [SerializeField] protected PlayerInputHandler _input = null;

    private void Awake()
    {
        DisableCollision();
    }

    public virtual void GatherInput()
    {
        if (_input.AttackPerformed)
        {
            Attack();
            _sfx.Play(transform.position);
        }
    }

    public void Attack()
    {
        if (IsAttacking()) return;
        StartCoroutine(Attack_Routine());
    }

    private IEnumerator Attack_Routine()
    {
        EnableCollision();
        yield return new WaitForSeconds(_time);
        DisableCollision();
    }

    public void EnableCollision()
    {
        SetEnable(true);
    }

    public void DisableCollision()
    {
        SetEnable(false);
    }

    private void SetEnable(bool _value)
    {
        _collider.enabled = _value;
    }

    public bool IsAttacking()
    {
        return _collider.isActiveAndEnabled;
    }
}
