using System.Collections;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] MeleeWeapon _standSword = null;
    [SerializeField] MeleeWeapon _crouchSword = null;

    //[SerializeField] GameObject _sword = null;
    //[SerializeField] float _time = 0.5f;

    public MeleeWeapon StandSword { get => _standSword; }
    public MeleeWeapon CrouchSword { get => _crouchSword; }

    //private void Awake()
    //{
    //    EndAttack();
    //}

    //public void GatherInput()
    //{
    //    bool _input = Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1");

    //    if (_input)
    //    {
    //        Attack();
    //    }
    //}

    //private void Attack()
    //{
    //    if (IsAttacking()) return;

    //    StartCoroutine(Attack_Routine());
    //}

    //private IEnumerator Attack_Routine()
    //{
    //    StartAttack();
    //    yield return new WaitForSeconds(_time);
    //    EndAttack();
    //}

    //private void StartAttack()
    //{
    //    _sword.SetActive(true);
    //}

    //private void EndAttack()
    //{
    //    _sword.SetActive(false);
    //}

    public bool IsAttacking()
    {
        return _standSword.IsAttacking() || _crouchSword.IsAttacking();
        //return _sword.activeInHierarchy;
    }
}
