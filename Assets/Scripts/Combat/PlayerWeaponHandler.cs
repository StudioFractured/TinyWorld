using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] MeleeWeapon _standSword = null;
    [SerializeField] MeleeWeapon _crouchSword = null;
    [Space]
    [SerializeField] ShieldWeapon _standShield = null;
    [SerializeField] ShieldWeapon _crouchShield = null;
    [Space]
    [SerializeField] List<DamageOnHit> _damageAgents = null;

    public void GatherStandInput()
    {
        if (!IsDefending())
        {
            _standSword.GatherInput();
        }

        if (!IsAttacking())
        {
            _standShield.GatherInput();
        }
    }

    public void GatherCrouchInput()
    {
        if (!IsDefending())
        {
            _crouchSword.GatherInput();
        }

        if (!IsAttacking())
        {
            _crouchShield.GatherInput();
        }
    }

    public void DisableAll()
    {
        _standSword.DisableCollision();
        _crouchSword.DisableCollision();
        _standShield.DisableCollision();
        _crouchShield.DisableCollision();
    }

    public bool IsAttacking()
    {
        return _standSword.IsAttacking() || _crouchSword.IsAttacking();
    }

    public bool IsDefending()
    {
        return _standShield.IsAttacking() || _crouchShield.IsAttacking();
    }

    public void IncreaseDamage()
    {
        int _count = _damageAgents.Count;

        for (int i = 0; i < _count; i++)
        {
            _damageAgents[i].IncreaseDamage();
        }
    }
}
