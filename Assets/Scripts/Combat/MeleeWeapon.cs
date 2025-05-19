using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    //[SerializeField] GameObject _collider = null;
    [SerializeField] SpriteRenderer _renderer = null;
    [SerializeField] Collider2D _collider = null;
    [SerializeField] float _time = 0.5f;

    private void Awake()
    {
        EndAttack();
    }

    public void GatherInput()
    {
        bool _input = Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1");

        if (_input)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (IsAttacking()) return;
        StartCoroutine(Attack_Routine());
    }

    private IEnumerator Attack_Routine()
    {
        StartAttack();
        yield return new WaitForSeconds(_time);
        EndAttack();
    }

    private void StartAttack()
    {
        SetEnable(true);
    }

    private void EndAttack()
    {
        SetEnable(false);
    }

    private void SetEnable(bool _value)
    {
        _renderer.enabled = _value;
        _collider.enabled = _value;
    }

    public bool IsAttacking()
    {
        return _collider.isActiveAndEnabled;
    }
}
