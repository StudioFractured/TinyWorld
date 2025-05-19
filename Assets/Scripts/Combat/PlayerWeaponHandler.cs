using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] MeleeWeapon _standSword = null;
    [SerializeField] MeleeWeapon _crouchSword = null;
    [SerializeField] ShieldWeapon _standShield = null;
    [SerializeField] ShieldWeapon _crouchShield = null;

    public MeleeWeapon StandSword { get => _standSword; }
    public MeleeWeapon CrouchSword { get => _crouchSword; }
    public ShieldWeapon StandShield { get => _standShield; }
    public ShieldWeapon CrouchShield { get => _crouchShield; }

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

    public void DisableShields()
    {
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
}
